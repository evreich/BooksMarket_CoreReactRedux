using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } 

        public User()
        {
            Orders = new List<Order>(); 
        }

        public User(string userName, string fullName) : base(userName)
        {
            FullName = fullName;
            Orders = new List<Order>();
        }
    }
}
