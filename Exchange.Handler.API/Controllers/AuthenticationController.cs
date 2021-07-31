using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Exchange.Handler.Helpers;
using Exchange.Handler.Models;
using Exchange.Handler.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Exchange.Handler.Repository.Entities;

namespace Exchange.Handler.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthenticationController(
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AccountLoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new Person() { FirstName = loginModel.Username, LastName = loginModel.Password };

                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.FirstName.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);


                return Ok(new
                {
                    //Id = user.Id,
                    Username = loginModel.Username,
                    FirstName = user.FirstName,
                    //LastName = user.LastName,
                    Token = tokenString
                });
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.GetError());
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
