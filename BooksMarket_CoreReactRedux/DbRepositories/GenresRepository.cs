using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.EF;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly BooksContext _context;

        public GenresRepository(BooksContext booksContext)
        {
            _context = booksContext;
        }

        public async Task AddElem(Genre elem)
        {
            await _context.Genres.AddAsync(elem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteElem(Genre elem)
        {
            _context.Genres.Remove(elem);
            await _context.SaveChangesAsync();
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

        public async Task EditElem(Genre elem)
        {
            _context.Genres.Update(elem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksCurrGenre(int id)
        {
            var currGenre = await _context.Genres.SingleAsync(genre => genre.Id == id);
            return currGenre.Books;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.Include(genre => genre.Books).ToListAsync();
        }

        public async Task<Genre> GetElem(int id)
        {
            return await _context.Genres.Include(genre => genre.Books).SingleAsync(Genre => Genre.Id == id);
        }
    }
}
