using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Identity;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public class DbRouteElementsUpdater: DBUpdater<BooksContext, RouteElement>
    {
        private readonly IRouteElementsRepository _routeElementRepository;
        private readonly RoleManager<UserRole> _roleManager;

        public DbRouteElementsUpdater(BooksContext context, IRouteElementsRepository routeElementRepository, RoleManager<UserRole> roleManager) 
            : base(context)
        {
            _routeElementRepository = routeElementRepository;
            _roleManager = roleManager;
        }

        public override async Task Init()
        {
            if (!_context.RouteElements.Any())
            {
                await EnsureRouteElements();
            }
            if (!_context.UserRoleRouteElements.Any())
            {
                await EnsureUserRoleRouteElements();
            }
        }

        public override void Update(List<RouteElement> items)
        {
            _routeElementRepository.AddRouteElements(items);
        }

        public async Task EnsureUserRoleRouteElements()
        {
            foreach (var routes in StandartIdentityDataConstants.RoutesForRoles)
            {
                var role = await _roleManager.FindByNameAsync(routes.Role);
                var countElemsForRole = (await _routeElementRepository.GetRouteElementsForRole(role.Name)).Count;
                if (countElemsForRole == 0 || countElemsForRole < routes.RouteElements.Count)
                {
                    _routeElementRepository.ClearRelationsBetweenRoleAndRouteElems(role.Id);
                    await _routeElementRepository.SetRouteElementsForRole(role.Id, routes.RouteElements);
                }
            }
        }

        public async Task EnsureRouteElements()
        {
            var routeItems = StandartIdentityDataConstants.StandartRoutes;

            if (_routeElementRepository.GetCountRouteElements() < routeItems.Count)
            {
                _routeElementRepository.ClearAllElems();
                await _routeElementRepository.AddRouteElements(routeItems);
            }
        }
    }
}
