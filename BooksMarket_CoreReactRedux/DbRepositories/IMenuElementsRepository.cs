using BooksMarket_CoreReactRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public interface IMenuElementsRepository: IDisposable
    {
        int GetCountMenuElements();
        void ClearAllElems();
        void ClearRelationsBetweenRoleAndMenuElems(string roleId);
        Task AddMenuElements(List<MenuElement> elems);
        List<MenuElement> GetMenuElementsForRole(string roleName);
        Task SetMenuElementsForRole(string roleId, List<MenuElement> elems);
    }
}
