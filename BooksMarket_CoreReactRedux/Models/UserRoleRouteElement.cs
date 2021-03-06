﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class UserRoleRouteElement
    {
        [Key]
        public int RouteElementId { get; set; }
        [Key]
        public string UserRoleId { get; set; }
        
        public RouteElement RouteElement { get; set; }
        
        public UserRole UserRole { get; set; }
    }
}
