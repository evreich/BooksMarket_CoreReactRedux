using BooksMarket_CoreReactRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.EF.SeedDbHelpers
{
    public static class StandartIdentityDataConstants
    {
        public struct UserInfo
        {
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string RoleName { get; set; }
        }

        public const string USER_ROLE = "user";
        public const string BOOKKEEPER_ROLE = "bookkeeper";
        public const string STOREKEEPER_ROLE = "storekeeper";
        public const string ADMIN_ROLE = "admin";

        public static List<string> StandartRoles = new List<string>
        {
            ADMIN_ROLE, USER_ROLE, BOOKKEEPER_ROLE, STOREKEEPER_ROLE
        };

        public static List<UserInfo> StandartUsers = new List<UserInfo>
        {
            new UserInfo
            {
                UserName = "admin@list.ru",
                FullName = "Администратор",
                Password = "admin",
                RoleName = ADMIN_ROLE
            },
            new UserInfo
            {
                UserName = "bookkeeper@list.ru",
                FullName = "Библиотекарь",
                Password = "bookkeeper",
                RoleName = BOOKKEEPER_ROLE
            },
            new UserInfo
            {
                UserName = "storekeeper@list.ru",
                FullName = "Кладовщик",
                Password = "storekeeper",
                RoleName = STOREKEEPER_ROLE
            },
            new UserInfo
            {
                UserName = "vlasov_ms@list.ru",
                FullName = "Власов Максим Сергеевич",
                Password = "vlasov",
                RoleName = USER_ROLE
            }
        };

        private static readonly RouteElement RouteElem_Books = new RouteElement { Component = "Books", Path = "/books" };
        private static readonly RouteElement RouteElem_Genres = new RouteElement { Component = "Genres", Path = "/genres" };
        private static readonly RouteElement RouteElem_Orders = new RouteElement { Component = "Orders", Path = "/orders" };
        private static readonly RouteElement RouteElem_Users = new RouteElement { Component = "Users", Path = "/users" };
        private static readonly RouteElement RouteElem_ReturnedBooks = new RouteElement { Component = "ReturnedBooks", Path = "/returnedBooks" };
        private static readonly RouteElement RouteElem_Market_Books = new RouteElement { Component = "MarketBooks", Path = "/market/books" };
        private static readonly RouteElement RouteElem_Market_Orders = new RouteElement { Component = "MarketOrders", Path = "/market/orders" };
        private static readonly RouteElement RouteElem_Market_StoreBooks = new RouteElement { Component = "MarketStoreBooks", Path = "/market/storeBooks" };
        private static readonly RouteElement RouteElem_Market_Cart = new RouteElement { Component = "MarketCart", Path = "/market/cart" };

        public static List<RouteElement> StandartRoutes = new List<RouteElement>
        {
            RouteElem_Books, RouteElem_Genres, RouteElem_Orders, RouteElem_Users,RouteElem_ReturnedBooks,
            RouteElem_Market_Books, RouteElem_Market_Orders, RouteElem_Market_StoreBooks, RouteElem_Market_Cart
        };

        public struct RouteForRole
        {
            public string Role { get; set; }
            public List<RouteElement> RouteElements { get; set; }
        }

        public static List<RouteForRole> RoutesForRoles = new List<RouteForRole>
        {
            new RouteForRole
            {
                Role = ADMIN_ROLE,
                RouteElements = new List<RouteElement>
                {
                    RouteElem_Books, RouteElem_Genres, RouteElem_Orders, RouteElem_Users, RouteElem_ReturnedBooks
                }
            },
            new RouteForRole
            {
                Role = USER_ROLE,
                RouteElements = new List<RouteElement>
                {
                    RouteElem_Market_Books, RouteElem_Market_StoreBooks, RouteElem_Market_Orders, RouteElem_Market_Cart
                }
            },
            new RouteForRole
            {
                Role = BOOKKEEPER_ROLE,
                RouteElements = new List<RouteElement>
                {
                    RouteElem_Orders, RouteElem_ReturnedBooks
                }
            },
            new RouteForRole
            {
                Role = STOREKEEPER_ROLE,
                RouteElements = new List<RouteElement>
                {
                    RouteElem_Books, RouteElem_Genres
                }
            }
        };

        private static readonly MenuElement MenuElem_BookMarket_Logo = new MenuElement { Title = "BookMarket", Path = "/" };
        private static readonly MenuElement MenuElem_Books = new MenuElement { Title = "Книги", Path = "/books" };
        private static readonly MenuElement MenuElem_Genres = new MenuElement { Title = "Жанры", Path = "/genres" };
        private static readonly MenuElement MenuElem_Orders = new MenuElement { Title = "Заказы", Path = "/orders" };
        private static readonly MenuElement MenuElem_Users = new MenuElement { Title = "Пользователи", Path = "/users" };
        private static readonly MenuElement MenuElem_ReturnedBooks = new MenuElement { Title = "Возврат", Path = "/returnedBooks" };
        private static readonly MenuElement MenuElem_Market_Books = new MenuElement { Title = "Книги", Path = "/market/books" };
        private static readonly MenuElement MenuElem_Market_Orders = new MenuElement { Title = "Заказы", Path = "/market/orders" };
        private static readonly MenuElement MenuElem_Market_StoreBooks = new MenuElement { Title = "Хранилище книг", Path = "/market/storeBooks" };
        private static readonly MenuElement MenuElem_Market_Cart = new MenuElement { Title = "Корзина", Path = "/market/cart" };

        public static List<MenuElement> StandartMenuElements = new List<MenuElement>
        {
            MenuElem_Books, MenuElem_Genres, MenuElem_Orders, MenuElem_Users, MenuElem_ReturnedBooks, MenuElem_Market_Books,
            MenuElem_Market_Orders, MenuElem_Market_StoreBooks,MenuElem_Market_Cart, MenuElem_BookMarket_Logo
        };

        public struct MenuForRole
        {
            public string Role { get; set; }
            public List<MenuElement> MenuElements { get; set; }
        }

        public static List<MenuForRole> MenuForRoles = new List<MenuForRole>
        {
            new MenuForRole
            {
                Role = ADMIN_ROLE,
                MenuElements = new List<MenuElement>
                {
                    MenuElem_BookMarket_Logo, MenuElem_Books, MenuElem_Genres, MenuElem_Orders, MenuElem_ReturnedBooks, MenuElem_Users
                }
            },
            new MenuForRole
            {
                Role = USER_ROLE,
                MenuElements = new List<MenuElement>
                {
                    MenuElem_BookMarket_Logo, MenuElem_Market_Books, MenuElem_Market_StoreBooks, MenuElem_Market_Orders, MenuElem_Market_Cart
                }
            },
            new MenuForRole
            {
                Role = BOOKKEEPER_ROLE,
                MenuElements = new List<MenuElement>
                {
                    MenuElem_BookMarket_Logo, MenuElem_Orders, MenuElem_ReturnedBooks
                }
            },
            new MenuForRole
            {
                Role = STOREKEEPER_ROLE,
                MenuElements = new List<MenuElement>
                {
                    MenuElem_BookMarket_Logo, MenuElem_Books, MenuElem_Genres
                }
            }
        };
    }
}
