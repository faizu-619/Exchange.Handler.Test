
using Microsoft.EntityFrameworkCore;
using Exchange.Handler.Repository;
using Exchange.Handler.Repository.Entities;
using Exchange.Handler.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Handler.Services
{
    public class TradeService : ITradeService
    {
        private DataContext _context;

        public TradeService(DataContext context)
        {
            _context = context;
        }

        public Trade GetById(int id)
        {
            return _context.Trades
            .Include(x => x.TradeType)
            .FirstOrDefault(x => x.Id == id);
        }

        public Trade Create(Trade trade)
        {
            // validation
            if (trade == null)
            {
                throw new RepositoryException("Trade is required");
            }

            if (string.IsNullOrWhiteSpace(trade.Title))
            {
                throw new RepositoryException("Title is required");
            }

            if (_context.Trades.Any(x => x.Title == trade.Title))
            {
                throw new RepositoryException("Duplicate trade name.");
            }

            trade.IsActive = true;
            trade.CreatedDate = DateTime.UtcNow;

            _context.Trades.Add(trade);
            _context.SaveChanges();

            return trade;
        }

        public void Update(Trade trade)
        {
            var existingProduct = _context.Trades.Find(trade.Id);

            if (existingProduct == null)
            {
                throw new RepositoryException("Trade not found");
            }

            if (trade.Title != existingProduct.Title)
            {
                if (_context.Trades.Any(x => x.Title == trade.Title))
                {
                    throw new RepositoryException("Duplicate trade name.");
                }
            }

            // update user properties
            existingProduct.Title = trade.Title;
            existingProduct.Description = trade.Description;
            existingProduct.TradeTypeId = trade.TradeTypeId;

            existingProduct.ModifiedDate = DateTime.UtcNow;

            _context.Trades.Update(existingProduct);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Trades.Find(id);
            if (product != null)
            {
                _context.Trades.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
