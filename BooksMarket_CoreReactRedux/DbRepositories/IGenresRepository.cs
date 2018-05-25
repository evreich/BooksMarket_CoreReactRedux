using BooksMarket_CoreReactRedux.Models;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface IGenresRepository: ICRUDRepository<Genre>, IDisposable
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Book>> GetAllBooksCurrGenre(int id);
    }
}
