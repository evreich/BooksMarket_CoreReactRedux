using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class BooksAndGenresSeedData
    {
        public static async Task EnsurePopulated(IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (BooksContext context = services.GetRequiredService<BooksContext>())
                {
                    try
                    {
                        if (!context.Genres.Any())
                        {
                            DbGenresUpdater updater = new DbGenresUpdater(context);
                            await updater.Init();
                        }
                        if (!context.Books.Any())
                        {
                            DbBooksUpdater updater = new DbBooksUpdater(context);
                            await updater.Init();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = service.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
            }
        }
    }
}
