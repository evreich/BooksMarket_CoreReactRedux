using BooksMarket_CoreReactRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface IOrdersRepository: ICRUDRepository<Order>, IDisposable
    {
        IEnumerable<Order> GetNotConfirmedOrders();
        IEnumerable<Order> GetOrdersForUser(string userId);
        void ConfirmOrder(int orderID);
        void CancelOrder(int orderID);
    }
}
