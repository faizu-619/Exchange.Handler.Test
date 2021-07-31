using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Handler.API.Models
{
    public class SellingModel
    {
        public int TradeId { get; set; }
        public int Quantity { get; set; }
    }

    public class SellingModelValidation : AbstractValidator<SellingModel>
    {
        public SellingModelValidation()
        {
            RuleFor(x => x.TradeId).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).NotNull().NotEmpty();

        }
    }
}
