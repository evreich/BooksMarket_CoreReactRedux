using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class DbUsersUpdater
    {
        private readonly UserManager<User> _userManager;
        private readonly BooksContext _context;

        public DbUsersUpdater(BooksContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Init()
        {
            if (!_context.Users.Any())
            {
                await EnsureUsers();
            }
        }

        public void Update(List<User> items)
        {
            items.ForEach(item => _userManager.CreateAsync(item));
        }

        public async Task EnsureUsers()
        {
            foreach (var info in StandartIdentityDataConstants.StandartUsers)
            {
                if (await _userManager.FindByNameAsync(info.UserName) == null)
                {
                    User user = new User(info.UserName, info.FullName)
                    {
                        Email = info.UserName,
                        FullName = info.FullName
                    };
                    user.SecurityStamp = Guid.NewGuid().ToString("D");
                    var result = await _userManager.CreateAsync(user, info.Password).ConfigureAwait(false);
                    await _userManager.AddToRoleAsync(user, info.RoleName);
                }
            };
        }
    }
}
