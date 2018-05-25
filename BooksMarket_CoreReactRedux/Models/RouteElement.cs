using Newtonsoft.Json;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Models
{
    public class RouteElement: DBModel
    {
        [Required]
        public string Component { get; set; }

        [Required]
        public string Path { get; set; }

        [NotMapped]
        public bool IsAccessable { get; set; } = false;

        [JsonIgnore]
        public ICollection<UserRoleRouteElement> UserRoleRouteElements { get; set; }

        public RouteElement()
        {
            UserRoleRouteElements = new List<UserRoleRouteElement>();
        }

        public RouteElement(string component, string path)
        {
            Component = component;
            Path = path;
            UserRoleRouteElements = new List<UserRoleRouteElement>();
        }
    }

    public class RouterElementComparer : IEqualityComparer<RouteElement>
    {

        public bool Equals(RouteElement x, RouteElement y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
 
            return x != null && y != null && x.Id.Equals(y.Id);
        }

        public int GetHashCode(RouteElement obj)
        { 
            int hashProductName = obj.Component.GetHashCode();

            int hashProductCode = obj.Path.GetHashCode();

            return hashProductName ^ hashProductCode;
        }
    }
}
