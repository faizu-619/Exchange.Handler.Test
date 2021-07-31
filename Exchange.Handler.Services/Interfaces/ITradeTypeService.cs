using Exchange.Handler.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Handler.Services
{
    public interface ITradeTypeService
    {
        IEnumerable<TradeType> GetAll();
        TradeType GetById(int id);
        TradeType Create(TradeType tradeType);
        void Update(TradeType tradeType);
        void Delete(int id);
    }
}
