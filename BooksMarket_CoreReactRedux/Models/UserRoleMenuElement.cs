using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class UserRoleMenuElement
    {
        [Key]
        public int MenuElementId { get; set; }
        [Key]
        public string UserRoleId { get; set; }
       
        public MenuElement MenuElement { get; set; }
        
        public UserRole UserRole { get; set; }
    }
}
