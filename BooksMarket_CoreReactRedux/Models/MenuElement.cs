using Newtonsoft.Json;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class MenuElement: DBModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Path { get; set; }

        [JsonIgnore]
        public ICollection<UserRoleMenuElement> UserRoleMenuElements { get; set; }

        public MenuElement()
        {
            UserRoleMenuElements = new List<UserRoleMenuElement>();
        }

        public MenuElement(string title, string path)
        {
            Title = title;
            Path = path;
            UserRoleMenuElements = new List<UserRoleMenuElement>();
        }
    }
}
