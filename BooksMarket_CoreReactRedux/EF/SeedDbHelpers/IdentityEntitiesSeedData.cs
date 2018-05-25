using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class IdentityEntitiesSeedData
    {
        public static async Task EnsurePopulated(IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var rolesManager = services.GetRequiredService<RoleManager<UserRole>>();
                    using (var booksContext = services.GetRequiredService<BooksContext>())
                    {
                        if (!services.GetRequiredService<BooksContext>().UserRoles.Any())
                        {
                            DbUserRolesUpdater updater = new DbUserRolesUpdater(
                                services.GetRequiredService<BooksContext>(), 
                                rolesManager);
                            await updater.Init();
                        }
                        if (!services.GetRequiredService<BooksContext>().Users.Any())
                        {
                            DbUsersUpdater updater = new DbUsersUpdater(
                                booksContext,
                                services.GetRequiredService<UserManager<User>>());
                            await updater.Init();
                        }
                        if (!services.GetRequiredService<BooksContext>().MenuElements.Any())
                        {
                            DbMenuElementsUpdater updater = new DbMenuElementsUpdater(
                                booksContext,
                                services.GetRequiredService<IMenuElementsRepository>(),
                                rolesManager);
                            await updater.Init();
                        }
                        if (!services.GetRequiredService<BooksContext>().RouteElements.Any())
                        {
                            DbRouteElementsUpdater updater = new DbRouteElementsUpdater(
                                booksContext,
                                services.GetRequiredService<IRouteElementsRepository>(),
                                rolesManager);
                            await updater.Init();
                        }
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
