
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class UserRole : IdentityRole
    {
        [JsonIgnore]
        public ICollection<UserRoleMenuElement> UserRoleMenuElements { get; set; } = new List<UserRoleMenuElement>();

        [JsonIgnore]
        public ICollection<UserRoleRouteElement> UserRoleRouteElements { get; set; } = new List<UserRoleRouteElement>();

        public UserRole(string roleName) : base(roleName) { }

        public UserRole() : base() { }
    }
}
