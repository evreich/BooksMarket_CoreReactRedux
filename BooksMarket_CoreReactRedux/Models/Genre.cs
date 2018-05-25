using Newtonsoft.Json;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class Genre: DBModel
    {
        [Required]
        public string Title { get; set; }
        [JsonIgnore]
        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
