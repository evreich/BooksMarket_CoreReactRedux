using BooksMarket_CoreReactRedux.EF;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public class OrdersRepository: IOrdersRepository
    {
        private readonly BooksContext _context;

        public OrdersRepository(BooksContext context)
        {
            _context = context;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Order> GetElem(int id)
        {
            return await _context.Orders
                .Include(o => o.BookOrders)
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddElem(Order elem)
        {
            await _context.Orders.AddAsync(elem);
            await _context.SaveChangesAsync();
        }

        public async Task EditElem(Order elem)
        {
            _context.Update(elem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteElem(Order elem)
        {
            _context.Remove(elem);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Order> GetNotConfirmedOrders()
        {
            return _context.Orders
                .Where(o => o.IsSuccess == null)
                .Include(o => o.User);
        }

        public IEnumerable<Order> GetOrdersForUser(string userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User);
        }

        public void ConfirmOrder(int orderID)
        {
            var order = _context.Orders.Include(o => o.BookOrders).SingleOrDefault(o => o.Id == orderID);
            order.IsSuccess = true;
            _context.Update(order);
            _context.SaveChanges();
        }

        public void CancelOrder(int orderID)
        {
            var order = _context.Orders.Include(o => o.BookOrders).SingleOrDefault(o => o.Id == orderID);
            order.IsSuccess = false;
            _context.Update(order);
            _context.SaveChanges();
        }
    }
}
