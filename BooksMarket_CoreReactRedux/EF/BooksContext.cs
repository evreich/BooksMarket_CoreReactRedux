using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF
{
    public class BooksContext : IdentityDbContext<User, UserRole, string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<MenuElement> MenuElements { get; set; }
        public DbSet<UserRoleMenuElement> UserRoleMenuElements { get; set; }
        public DbSet<RouteElement> RouteElements { get; set; }
        public DbSet<UserRoleRouteElement> UserRoleRouteElements { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many-to-many для книг и заказов
            modelBuilder.Entity<BookOrder>()
                .HasKey(bc => new { bc.BookId, bc.OrderId });

            modelBuilder.Entity<BookOrder>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookOrders)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookOrder>()
                .HasOne(bc => bc.Order)
                .WithMany(c => c.BookOrders)
                .HasForeignKey(bc => bc.OrderId);

            //many-to-many для ролей и связанных пунктов меню
            modelBuilder.Entity<UserRoleMenuElement>()
                .HasKey(user_menu => new { user_menu.UserRoleId, user_menu.MenuElementId });

            modelBuilder.Entity<UserRoleMenuElement>()
                .HasOne(user_menu => user_menu.MenuElement)
                .WithMany(b => b.UserRoleMenuElements)
                .HasForeignKey(user_menu => user_menu.MenuElementId);

            modelBuilder.Entity<UserRoleMenuElement>()
                .HasOne(user_menu => user_menu.UserRole)
                .WithMany(c => c.UserRoleMenuElements)
                .HasForeignKey(user_menu => user_menu.UserRoleId);

            //many-to-many для ролей и доступных маршрутов
            modelBuilder.Entity<UserRoleRouteElement>()
                .HasKey(user_role => new { user_role.UserRoleId, user_role.RouteElementId });

            modelBuilder.Entity<UserRoleRouteElement>()
                .HasOne(user_role => user_role.UserRole)
                .WithMany(b => b.UserRoleRouteElements)
                .HasForeignKey(user_role => user_role.UserRoleId);

            modelBuilder.Entity<UserRoleRouteElement>()
                .HasOne(user_role => user_role.RouteElement)
                .WithMany(c => c.UserRoleRouteElements)
                .HasForeignKey(user_role => user_role.RouteElementId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
