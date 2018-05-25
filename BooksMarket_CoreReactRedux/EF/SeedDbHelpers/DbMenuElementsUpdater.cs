using BooksMarket_CoreReactRedux.DbRepositories;
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
    public class DbMenuElementsUpdater : DBUpdater<BooksContext, MenuElement>
    {
        private readonly IMenuElementsRepository _menuElementRepository;
        private readonly RoleManager<UserRole> _roleManager;

        public DbMenuElementsUpdater(BooksContext context, IMenuElementsRepository menuElementRepository, RoleManager<UserRole> roleManager) : base(context)
        {
            _menuElementRepository = menuElementRepository;
            _roleManager = roleManager;
        }

        public override async Task Init()
        {
            if (!_context.MenuElements.Any())
            {
                await EnsureMenuElements();
            }
            if (!_context.UserRoleMenuElements.Any())
            {
                await EnsureUserRoleMenuElements();
            }
        }

        public override void Update(List<MenuElement> items)
        {
            _menuElementRepository.AddMenuElements(items);
        }
        public async Task EnsureUserRoleMenuElements()
        {
            var MenuForRoles = StandartIdentityDataConstants.MenuForRoles;

            foreach (var menu in MenuForRoles)
            {
                var role = await _roleManager.FindByNameAsync(menu.Role);
                var countElemsForRole = _menuElementRepository.GetMenuElementsForRole(role.Name).Count;
                if (countElemsForRole == 0 || countElemsForRole < menu.MenuElements.Count)
                {
                    _menuElementRepository.ClearRelationsBetweenRoleAndMenuElems(role.Id);
                    await _menuElementRepository.SetMenuElementsForRole(role.Id, menu.MenuElements);
                }
            }
        }

        public async Task  EnsureMenuElements()
        {
            var menuItems = StandartIdentityDataConstants.StandartMenuElements;

            if ((_menuElementRepository.GetCountMenuElements()) < menuItems.Count)
            {
                _menuElementRepository.ClearAllElems();
                await _menuElementRepository.AddMenuElements(menuItems);
            }
        }
    }
}
