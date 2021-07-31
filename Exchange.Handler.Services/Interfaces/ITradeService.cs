using Exchange.Handler.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Handler.Services
{
    public interface ITradeService
    {
        Trade GetById(int id);
        Trade Create(Trade trade);
        void Update(Trade trade);
        void Delete(int id);
    }
}
