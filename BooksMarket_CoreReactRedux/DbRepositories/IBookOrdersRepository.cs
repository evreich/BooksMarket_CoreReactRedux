using BooksMarket_CoreReactRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface IBookOrdersRepository: IDisposable
    {
        void SetActualBooksForOrder(int orderID, List<BookOrder> actualBooks);
        IEnumerable<BookOrder> GetBooksOfOrder(int orderID);
        IEnumerable<Book> GetBooksForUser(string userID, bool? isReturned);
        IEnumerable<Book> GetReturnedBooks();
        void ReturnBook(int bookID);
        void ConfirmBook(int bookID);
        void CancelBook(int bookID);
    }
}
