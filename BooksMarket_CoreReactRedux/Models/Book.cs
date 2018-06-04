using Newtonsoft.Json;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class Book: DBModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateCreating { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int GenreId { get; set; }
        [JsonIgnore]
        public Genre Genre { get; set; }

        [Required]
        public int Count { get; set; }

        [JsonIgnore]
        public ICollection<BookOrder> BookOrders { get; set; } 
            
        public Book()
        {
            BookOrders = new List<BookOrder>();
        }    
    }
}
