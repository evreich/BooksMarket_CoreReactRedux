using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class DbUserRolesUpdater
    {
        private readonly RoleManager<UserRole>_roleManager;
        private readonly BooksContext _context;

        public DbUserRolesUpdater(BooksContext context, RoleManager<UserRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Init()
        {
            if (!_context.UserRoles.Any())
            {
                await EnsureRoles();
            }
        }

        public void Update(List<UserRole> items)
        {
            items.ForEach(item => _roleManager.CreateAsync(item));
        }

        private async Task EnsureRoles()
        {
            var roles = StandartIdentityDataConstants.StandartRoles;

            if (_roleManager.Roles.Count() < roles.Count)
            {
                foreach (var roleName in roles)
                {
                    await _roleManager.CreateAsync(new UserRole(roleName));
                }
            }
        }
    }
}
