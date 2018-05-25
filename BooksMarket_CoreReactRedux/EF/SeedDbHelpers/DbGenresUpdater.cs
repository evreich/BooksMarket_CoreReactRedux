using BooksMarket_CoreReactRedux.Models;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class DbGenresUpdater : DBUpdater<BooksContext, Genre>
    {
        public DbGenresUpdater(BooksContext context) : base(context)
        {
        }

        public override async Task Init()
        {
            if (!_context.Genres.Any())
            {
                await SetInitGenres();
            }
        }

        public override void Update(List<Genre> items)
        {
            _context.Genres.AddRange(items);
            _context.SaveChanges();
        }

        private async Task<List<Genre>> SetInitGenres()
        {
            List<Genre> genres = new List<Genre>() {
                new Genre
                {
                    Title = "Детектив"
                },
                new Genre
                {
                    Title = "Фантастика"
                },
                new Genre
                {
                    Title = "Роман"
                },
                new Genre
                {
                    Title = "Антиутопия"
                },
                new Genre
                {
                    Title = "Приключения"
                }

            };
            _context.Genres.AddRange(genres);
            await _context.SaveChangesAsync();
            return genres;
        }
    }
}
