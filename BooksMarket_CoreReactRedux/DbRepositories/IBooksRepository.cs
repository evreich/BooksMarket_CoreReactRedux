using BooksMarket_CoreReactRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface IBooksRepository: ICRUDRepository<Book>, IDisposable
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetBooksByGenre(string genreTitle);
        void IncCountBooks(Book book);
        void IncCountBooks(Book book, int count);
        int DecCountBook(Book book);
        int DecCountBook(Book book, int count);
        Task<IEnumerable<Book>> GetBooksByFilter(string filter, List<Genre> genres);
        Task<IEnumerable<Book>> GetBooksByFilterInSomeGenre(string filter, string genreTitle);
        IEnumerable<Book> GetBooksForPage(IEnumerable<Book> books, int page, int CountElemsOnPage);
    }
}
