using BooksMarket_CoreReactRedux.EF;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public class RouteElementsRepository : IRouteElementsRepository
    {
        private readonly BooksContext _context;

        public RouteElementsRepository(BooksContext context)
        {
            _context = context;
        }

        public RouteElementsRepository()
        {
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

        public async Task<IEnumerable<RouteElement>> GetAllRouteElems() => await _context.RouteElements.ToListAsync();

        public int GetCountRouteElements() => _context.RouteElements.Count();

        public void ClearAllElems()
        {
            _context.RouteElements.RemoveRange(_context.RouteElements);
            _context.SaveChanges();
        } 

        public void ClearRelationsBetweenRoleAndRouteElems(string roleId)
        {
            _context.UserRoleRouteElements
                .RemoveRange(_context.UserRoleRouteElements.Where(elem => elem.UserRoleId == roleId));

            _context.SaveChanges();
        }

        public async Task AddRouteElements(List<RouteElement> elems)
        {
            await _context.RouteElements.AddRangeAsync(elems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RouteElement>> GetRouteElementsForRole(string roleName)
        {
            return await
                _context.UserRoleRouteElements
                .Include(item => item.UserRole)
                .Include(item => item.RouteElement)
                .Where(item => item.UserRole.Name == roleName)
                .Select(item => item.RouteElement)
                .ToListAsync();
        }

        private int GetRouteElementIdByAttr(string component, string path)
        {
            return _context.RouteElements
                .Where(route =>
                    route.Component == component &&
                    route.Path == path)
                .SingleOrDefault()
                .Id;
        }

        public async Task SetRouteElementsForRole(string roleId, List<RouteElement> elems)
        {
            elems.ForEach(elem =>
            {
                var routeElementId = GetRouteElementIdByAttr(elem.Component, elem.Path);

                _context.UserRoleRouteElements.Add(
                    new UserRoleRouteElement
                    {
                        RouteElementId = routeElementId,
                        UserRoleId = roleId
                    });
            });
            await _context.SaveChangesAsync();
        }
    }
}
