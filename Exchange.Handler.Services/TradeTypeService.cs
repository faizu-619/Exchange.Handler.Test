using Exchange.Handler.Repository;
using Exchange.Handler.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Handler.Services
{
    public class TradeTypeService : ITradeTypeService
    {
        private DataContext _context;

        public TradeTypeService(DataContext context)
        {
            _context = context;
        }

        public TradeType Create(TradeType productType)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeType> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TradeType productType)
        {
            throw new NotImplementedException();
        }

        TradeType ITradeTypeService.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
