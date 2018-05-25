using BooksMarket_CoreReactRedux.EF;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.DbRepositories
{
    public class MenuElementsRepository : IMenuElementsRepository
    {
        private readonly BooksContext _context;

        public MenuElementsRepository(BooksContext context)
        {
            _context = context;
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

        public int GetCountMenuElements() => _context.MenuElements.Count();

        public void ClearAllElems()
        {
            _context.MenuElements.RemoveRange(_context.MenuElements);
            _context.SaveChanges();
        }

        public void ClearRelationsBetweenRoleAndMenuElems(string roleId)
        {
            _context.UserRoleMenuElements
                .RemoveRange(_context.UserRoleMenuElements.Where(elem => elem.UserRoleId == roleId));
            _context.SaveChanges();
        }

        public async Task AddMenuElements(List<MenuElement> elems)
        {
            await _context.MenuElements.AddRangeAsync(elems);
            await _context.SaveChangesAsync();
        }

        public List<MenuElement> GetMenuElementsForRole(string roleName)
        {
            return  
                _context.UserRoleMenuElements
                .Include(item => item.UserRole)
                .Include(item => item.MenuElement)
                .Where(item => item.UserRole.Name == roleName)
                .Select(item => item.MenuElement).ToList();
        }

        private int GetMenuElementIdByAttr(string title, string path)
        {
            return _context.MenuElements
                .Where(elem =>
                    elem.Title == title &&
                    elem.Path == path)
                .SingleOrDefault()
                .Id;
        }

        public async Task SetMenuElementsForRole(string roleId, List<MenuElement> elems)
        {
           foreach (var elem in elems)
            {
                var menuElementId = GetMenuElementIdByAttr(elem.Title, elem.Path);

                _context.UserRoleMenuElements.Add(
                    new UserRoleMenuElement
                    {
                        MenuElementId = menuElementId,
                        UserRoleId = roleId
                    });
            }
           await _context.SaveChangesAsync();
        }
    }
}
