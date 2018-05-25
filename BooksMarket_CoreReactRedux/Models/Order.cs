using Newtonsoft.Json;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class Order: DBModel
    {
        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public bool? IsSuccess { get; set; }

        [Required]
        public DateTime DateReturn { get; set; }

        [JsonIgnore]
        public ICollection<BookOrder> BookOrders { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int HomeNumber { get; set; }

        [Required]
        public string PostIndex { get; set; }

        [Required]
        public bool GiftWrap { get; set; }

        public Order()
        {
            BookOrders = new List<BookOrder>();
        }
    }
}
