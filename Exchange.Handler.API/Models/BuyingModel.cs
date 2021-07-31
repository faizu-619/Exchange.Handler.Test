using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Handler.API.Models
{
    public class BuyingModel
    {
        public int TradeId { get; set; }
        public int Quantity { get; set; }
    }

    public class BuyingModelValidation : AbstractValidator<BuyingModel>
    {
        public BuyingModelValidation()
        {
            RuleFor(x => x.TradeId).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).NotNull().NotEmpty();

        }
    }
}
