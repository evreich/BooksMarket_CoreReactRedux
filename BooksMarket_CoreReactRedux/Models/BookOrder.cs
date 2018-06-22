using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class BookOrder
    {
        [Key]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Key]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public bool? IsReturned { get; set; }

        [Required]
        public int CountOfBook { get; set; }

        public BookOrder() { }
    }
}
