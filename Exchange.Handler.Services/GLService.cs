using Microsoft.EntityFrameworkCore;
using Exchange.Handler.Repository;
using Exchange.Handler.Repository.Entities;
using Exchange.Handler.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Handler.Services
{
    public class GLService : IGLService
    {
        private DataContext _context;

        public GLService(DataContext context)
        {
            _context = context;
        }

        public async Task<GL> Buy(GL item)
        {
            double balance = await Balance();

            if (balance > 0)
            {
                Trade trade = await _context.Trades.FirstAsync(x => x.Id == item.TradeId);

                switch (trade.TradeTypeId)
                {
                    case 1:
                        // get latest currency exchange value.
                        item.Debit = (item.Quantity * 100);
                        item.Credit = 0;
                        break;
                    case 2:
                        // call crypto service to get latest price.
                        item.Debit = (item.Quantity * 500);
                        item.Credit = 0;
                        break;
                    case 3:
                        // call crypto service to get latest price.
                        item.Debit = (item.Quantity * 500);
                        item.Credit = 0;
                        break;
                    default:
                        break;
                }
                await _context.AddAsync(item);
                _context.SaveChanges();
                return item;
            }
            else
            {
                throw new RepositoryException("Not enough balance.");
            }
        }

        public async Task<GL> Sell(GL item)
        {
            Trade trade = await _context.Trades.FirstAsync(x => x.Id == item.TradeId);

            switch (trade.TradeTypeId)
            {
                case 1:
                    // get latest currency exchange value.
                    item.Credit = (item.Quantity * 100);
                    item.Debit = 0;
                    break;
                case 2:
                    // call crypto service to get latest price.
                    item.Credit = (item.Quantity * 500);
                    item.Debit = 0;
                    break;
                case 3:
                    // call crypto service to get latest price.
                    item.Credit = (item.Quantity * 500);
                    item.Debit = 0;
                    break;
                default:
                    break;
            }
            await _context.AddAsync(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<double> Balance()
        {
            return (await _context.GL.SumAsync(x => x.Credit) - await _context.GL.SumAsync(x => x.Debit));
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GL>> GetTransactions()
        {
            return await Task.FromResult(_context.GL.Where(x => (x.IsActive && !x.IsDelete)));
        }

        public Task<bool> Update(GL item)
        {
            throw new NotImplementedException();
        }


        //public Tuple<IEnumerable<CartItem>, int> GetByFilter(CartFilters filters)
        //{
        //    Func<CartItem, bool> query = x => (filters.searchItems == null || filters.searchItems.Length < 1 || filters.searchItems.Contains(x.ProductId));

        //    int count = _context.CartItems.Count(query);

        //    if (count < 1)
        //    {
        //        throw new RepositoryException("No records found.");
        //    }

        //    var products = _context.CartItems
        //    .Where(query)
        //    .Skip((filters.pageIndex * filters.recordsCount))
        //    .Take(filters.recordsCount);

        //    return new Tuple<IEnumerable<CartItem>, int>(products, count);
        //}

        //public CartItem GetById(int id)
        //{
        //    return _context.CartItems
        //    .FirstOrDefault(x => x.Id == id);
        //}

        //public CartItem Create(CartItem cartItem)
        //{
        //    // validation
        //    if (cartItem == null)
        //    {
        //        throw new RepositoryException("Cart item is required");
        //    }

        //    if (cartItem.ProductId < 1)
        //    {
        //        throw new RepositoryException("Prodcut Id is required");
        //    }

        //    cartItem.IsActive = true;
        //    cartItem.CreatedDate = DateTime.UtcNow;

        //    _context.CartItems.Add(cartItem);
        //    _context.SaveChanges();

        //    return cartItem;
        //}

        //public void Update(CartItem cartItem)
        //{
        //    var existingItem = _context.CartItems.Find(cartItem.Id);

        //    if (existingItem == null)
        //    {
        //        throw new RepositoryException("CartItem not found");
        //    }

        //    //if (cartItem.ProductId != existingItem.ProductId)
        //    //{
        //    //    if (_context.CartItems.Any(x => x.ProductId == cartItem.ProductId))
        //    //    {
        //    //        throw new RepositoryException("Duplicate cart Item name.");
        //    //    }
        //    //}

        //    // update user properties
        //    existingItem.ProductId = cartItem.ProductId;
        //    existingItem.Price = cartItem.Price;
        //    existingItem.Quantity = cartItem.Quantity;
        //    existingItem.Discount = cartItem.Discount;

        //    existingItem.ModifiedDate = DateTime.UtcNow;

        //    _context.CartItems.Update(existingItem);
        //    _context.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    var cartItem = _context.CartItems.Find(id);
        //    if (cartItem != null)
        //    {
        //        _context.CartItems.Remove(cartItem);
        //        _context.SaveChanges();
        //    }
        //}

        //public IEnumerable<CartItem> GetAll()
        //{
        //    return _context.CartItems;
        //}
    }
}
