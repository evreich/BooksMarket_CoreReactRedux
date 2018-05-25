using BooksMarket_CoreReactRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface IRouteElementsRepository: IDisposable
    {
        int GetCountRouteElements();
        void ClearAllElems();
        void ClearRelationsBetweenRoleAndRouteElems(string roleId);
        Task AddRouteElements(List<RouteElement> elems);
        Task<List<RouteElement>> GetRouteElementsForRole(string roleName);
        Task SetRouteElementsForRole(string roleId, List<RouteElement> elems);
        Task<IEnumerable<RouteElement>> GetAllRouteElems();
    }
}
