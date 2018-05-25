using BooksMarket_CoreReactRedux.Models;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class DbBooksUpdater : DBUpdater<BooksContext, Book>
    {
        public DbBooksUpdater(BooksContext context) : base(context)
        {
        }

        public override async Task Init()
        {
            if (!_context.Books.Any())
            {
                await SetInitBooks(_context.Genres.ToList());
            }
        }

        public override void Update(List<Book> items)
        {
            _context.Books.AddRange(items);
            _context.SaveChanges();
        }

        private async Task<List<Book>> SetInitBooks(List<Genre> genres)
        {
            List<Book> books = new List<Book>() {
                new Book
                {
                    Title = "Оно",
                    DateCreating = new DateTime(1984,1,1),
                    Author = "Стивен Кинг",
                    Genre = genres[1],
                    Count = 3
                },
                new Book
                {
                    Title = "Шерлок Холмс",
                    DateCreating = new DateTime(1944,1,1),
                    Author = "Конан Дойль",
                    Genre = genres[0],
                    Count = 2
                },
                new Book
                {
                    Title = "Виноваты звезды",
                    DateCreating = new DateTime(1999,1,1),
                    Author = "Нора Робертс",
                    Genre = genres[2],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга1",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[2],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга2",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[3],
                    Count = 4
                },
                new Book
                {
                    Title = "Книга3",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[4],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга5",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[1],
                    Count = 3
                },
                new Book
                {
                    Title = "Книга1411",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[2],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга1213",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[4],
                    Count = 1
                },
                new Book
                {
                    Title = "Книга12342",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[3],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга15231",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[1],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга1632",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[4],
                    Count = 3
                },
                new Book
                {
                    Title = "Книга1765",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[3],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга12351561",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[0],
                    Count = 3
                },
                new Book
                {
                    Title = "Книга6231",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[0],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга43261",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[0],
                    Count = 1
                },
                new Book
                {
                    Title = "Книга1745",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[0],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга145127",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[4],
                    Count = 3
                },
                new Book
                {
                    Title = "Книга142352",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[2],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга1356136",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[2],
                    Count = 1
                },
                new Book
                {
                    Title = "Книга174522",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[3],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга134672",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[4],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга198563",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[0],
                    Count = 3
                },
                new Book
                {
                    Title = "Книга32631",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[2],
                    Count = 2
                },
                new Book
                {
                    Title = "Книга435631",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[1],
                    Count = 1
                },
                new Book
                {
                    Title = "Книга3454621",
                    DateCreating = new DateTime(1967,1,1),
                    Author = "Автор автор",
                    Genre = genres[4],
                    Count = 2
                }
            };
            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();
            return books;
        }
    }
}
