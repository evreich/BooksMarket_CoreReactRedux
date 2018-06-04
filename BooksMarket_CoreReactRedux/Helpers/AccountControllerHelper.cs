using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.Helpers
{
    public static class AccountControllerHelper
    {
        public static (List<Claim> claims, string token) GenerateJwtToken(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.FullName),
                new Claim(ClaimTypes.Role, role )
            };

            var key = AuthOptions.GetSymmetricSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(AuthOptions.LIFETIME));

            var token = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return (claims, new JwtSecurityTokenHandler().WriteToken(token));
        }

        public static async Task<IEnumerable<RouteElement>> GetRoutesForRole(string role, IRouteElementsRepository routeRepository)
        {
            var routesForRole = (await routeRepository.GetRouteElementsForRole(role))
                .Select(route => { route.IsAccessable = true; return route; })
                .ToList();
            var routesNotForRole = (await routeRepository.GetAllRouteElems())
                .Except(routesForRole, new RouterElementComparer());
            var allRoutes = routesNotForRole.Concat(routesForRole);
            return allRoutes;
        }
    }
}
