using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Exchange.Handler.Helpers;
using Exchange.Handler.Models;
using Exchange.Handler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exchange.Handler.API.Models;
using Exchange.Handler.Repository.Entities;
using Exchange.Handler.Repository.Helpers;

namespace Exchange.Handler.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IGLService _service;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public InvestmentController(
            IGLService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _service = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> Buy([FromBody] BuyingModel buyingModel)
        {
            var item = _mapper.Map<GL>(buyingModel);
            try
            {
                return Ok(await _service.Buy(item));
            }
            catch (RepositoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Sell")]
        public async Task<IActionResult> Sell([FromBody] SellingModel sellingModel)
        {
            var item = _mapper.Map<GL>(sellingModel);
            try
            {
                return Ok(await _service.Sell(item));
            }
            catch (RepositoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Balance")]
        public async Task<IActionResult> Balance()
        {
            try
            {
                return Ok(await _service.Balance());
            }
            catch (RepositoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
