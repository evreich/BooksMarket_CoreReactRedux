using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.EF;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksContext _context;
        public BooksRepository(BooksContext context)
        {
            _context = context;
        }

        public async Task<Book> GetElem(int id)
        {
            return await _context.Books
                .Include(book => book.Genre)
                .SingleAsync(book => book.Id == id);
        }

        public async Task AddElem(Book elem)
        {
            await _context.Books.AddAsync(elem);
            await _context.SaveChangesAsync();
        }

        public async Task EditElem(Book elem)
        {
            _context.Books.Update(elem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteElem(Book elem)
        {
            _context.Books.Remove(elem);
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

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(book => book.Genre)
                .ToListAsync();
        }

        public IEnumerable<Book> GetBooksForPage(IEnumerable<Book> books, int page, int CountElemsOnPage)
        {
            return books
                .OrderBy(book => book.Title)
                .Skip((page - 1) * CountElemsOnPage)
                .Take(CountElemsOnPage)
                .ToList();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(string genreTitle)
        {
            return await _context.Books
                .Include(book => book.Genre)
                .Where(book => book.Genre.Title.ToLower() == genreTitle.ToLower())
                .ToListAsync();
        }

        private async Task<IEnumerable<Book>> FilterBooks(string filter, List<Book> books)
        {
            if (DateTime.TryParse(filter, out DateTime res))
            {
                return await books.Where(book => book.DateCreating.Year == res.Year).AsQueryable().ToListAsync();
            }
            else if (int.TryParse(filter, out int bookYear))
            {
                return await books.Where(book => book.DateCreating.Year == bookYear).AsQueryable().ToListAsync();
            }
            else if (!string.IsNullOrEmpty(filter))
            {
                return await books.Where(book => book.Title.ToLower()
                .Contains(filter.ToLower()) || book.Author.ToLower().Contains(filter.ToLower()))
                .AsQueryable().ToListAsync();
            }
            else
                return new List<Book>();
        } 

        public async Task<IEnumerable<Book>> GetBooksByFilterInSomeGenre(string filter, string genreTitle)
        {
            IEnumerable<Book> books = await GetBooksByGenre(genreTitle);
            return await FilterBooks(filter, books.ToList());
        }

        public async Task<IEnumerable<Book>> GetBooksByFilter(string filter, List<Genre> genres)
        {
            List<Book> books = new List<Book>();
            if (genres.FirstOrDefault(genre => genre.Title.ToLower() == filter.ToLower()) != null)
            {
                return await _context
                    .Books
                    .Include(book => book.Genre)
                    .Where(book => book.Genre.Title.ToLower() == filter.ToLower())
                    .ToListAsync();
            }
            else
                return await FilterBooks(filter, books.ToList());
        }

        public void IncCountBooks(Book book)
        {
            book.Count++;
            _context.Update(book);
            _context.SaveChanges();
        }

        public void IncCountBooks(Book book, int count)
        {
            book.Count += count;
            _context.Update(book);
            _context.SaveChanges();
        }

        public int DecCountBook(Book book)
        {
            book.Count--;
            _context.Update(book);
            _context.SaveChanges();
            return book.Count;
        }

        public int DecCountBook(Book book, int count)
        {
            if (book.Count < count)
                throw new ArgumentException("Кол-во удаляемых книг превышает кол-во имеющихся");
            book.Count -= count;
            _context.Update(book);
            _context.SaveChanges();
            return book.Count;
        }
    }
}
