using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface ICRUDRepository<T> where T : DBModel
    {
        Task<T> GetElem(int id);
        Task AddElem(T elem);
        Task EditElem(T elem);
        Task DeleteElem(T elem);
    }
}
