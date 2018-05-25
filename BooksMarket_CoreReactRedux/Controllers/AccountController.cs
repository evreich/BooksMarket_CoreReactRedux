using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.EF.SeedDbHelpers;
using BooksMarket_CoreReactRedux.Models;
using BooksMarket_CoreReactRedux.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BooksMarket_CoreReactRedux.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IRouteElementsRepository _routeRepository;
        private readonly IMenuElementsRepository _menuRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager,
            IRouteElementsRepository routeRepository, IMenuElementsRepository menuRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _menuRepository = menuRepository;
            _routeRepository = routeRepository;
        }

        private string GenerateJwtToken(IdentityUser user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role )
            };

            var key = AuthOptions.GetSymmetricSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(AuthOptions.LIFETIME));

            var token = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IEnumerable<RouteElement>> GetRoutesForRole(string role)
        {
            var routesForRole = (await _routeRepository.GetRouteElementsForRole(role))
                .Select(route => { route.IsAccessable = true; return route; })
                .ToList();
            var routesNotForRole = (await _routeRepository.GetAllRouteElems())
                .Except(routesForRole, new RouterElementComparer());
            var allRoutes = routesNotForRole.Concat(routesForRole);
            return allRoutes;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(Registration regInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                Email = regInfo.Email,
                PhoneNumber = regInfo.Phone,
                FullName = regInfo.FullName
            };

            var result = await _userManager.CreateAsync(user, regInfo.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Создание пользователя не удалось. Обратитесь в техподдержку.");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            string role = StandartIdentityDataConstants.USER_ROLE;
            await _userManager.AddToRoleAsync(user, role);
            //отбор роутов для данной роли
            var allRoutes = await GetRoutesForRole(role);
            //получение пунктов меню для пользователя
            var menuElemsForRole = _menuRepository.GetMenuElementsForRole(role);
            var userFromDB = _userManager.Users.Single(u => u.Email == regInfo.Email);
            return Ok(
                new
                {
                    name = userFromDB.Email,
                    token = GenerateJwtToken(userFromDB, role),
                    role = new
                    {
                        name = StandartIdentityDataConstants.USER_ROLE,
                        headerMenuElements = menuElemsForRole,
                        routes = allRoutes
                    }
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userManager.Users.SingleOrDefault(u => u.Email == loginInfo.Email);
            if (user == null)
            {
                ModelState.AddModelError("NotUser", "Данный пользователь не зарегистрирован");
                return BadRequest(ModelState);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginInfo.Password, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("isNotLogged", "Неверный пароль");
                return BadRequest(ModelState);
            }
            
            var role = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(loginInfo.Email));
            if (role.Count != 1)
            {
                throw new Exception("Ошибка! Пользователь должен обладать одной ролью.");
            };

            //отбор роутов для данной роли
            var allRoutes = await GetRoutesForRole(role[0]);
            //получение пунктов меню для пользователя
            var menuElemsForRole = _menuRepository.GetMenuElementsForRole(role[0]);

            return Ok(
                new
                {
                    name = user.Email,
                    token = GenerateJwtToken(user, role[0]),
                    role = new
                    {
                        name = role[0],
                        headerMenuElements = menuElemsForRole,
                        routes = allRoutes
                    }
                });
        }

        [HttpGet]
        [Authorize]
        public IActionResult LogOff()
        {
            // чистим корзину
            //cart.Clear();
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = StandartIdentityDataConstants.ADMIN_ROLE)]
        public async Task<IActionResult> GetUsers()
        {
            List<object> usersList = new List<object>();
            foreach (User user in _userManager.Users)
            {
                var role = (await _userManager.GetRolesAsync(user))[0];
                usersList.Add(new { user, role });
            }
            return Ok(usersList);
        }

        [HttpGet]
        [Authorize(Roles = StandartIdentityDataConstants.ADMIN_ROLE)]
        public IActionResult GetRoles()
        {
            return Ok(_roleManager.Roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StandartIdentityDataConstants.ADMIN_ROLE)]
        public async Task<IActionResult> ChangeRole(string userId, string currRole)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Некорректный id пользователя.");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var oldRole = _roleManager.Roles.FirstOrDefault(role => role.Name == userRoles[0]).Name;

            await _userManager.AddToRolesAsync(user, new List<string> { currRole });
            await _userManager.RemoveFromRolesAsync(user, new List<string> { oldRole });

            return Ok(currRole);
        }
    }
}