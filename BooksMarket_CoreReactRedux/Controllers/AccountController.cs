using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.EF.SeedDbHelpers;
using BooksMarket_CoreReactRedux.Helpers;
using BooksMarket_CoreReactRedux.Models;
using BooksMarket_CoreReactRedux.RequestModels;
using Microsoft.AspNetCore.Antiforgery;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody]Registration regInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User(regInfo.Email, regInfo.FullName);

            var result = await _userManager.CreateAsync(user, regInfo.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Создание пользователя не удалось. Обратитесь в техподдержку.");
                return BadRequest(ModelState);
            }
            string role = StandartIdentityDataConstants.USER_ROLE;
            await _userManager.AddToRoleAsync(user, role);
            var userFromDB = _userManager.Users.Single(u => u.UserName == regInfo.Email);
            var userToken = AccountControllerHelper.GenerateJwtToken(userFromDB, role);

            //отбор роутов для данной роли
            var allRoutes = await AccountControllerHelper.GetRoutesForRole(role, _routeRepository);
            //получение пунктов меню для пользователя
            var menuElemsForRole = _menuRepository.GetMenuElementsForRole(role);

            var expireTime = Math.Round(DateTime.Now.ToUniversalTime().AddHours(AuthOptions.LIFETIME).Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds);

            return Ok(
                new
                {
                    name = userFromDB.UserName,
                    token = userToken.token,
                    expireTimeToken = expireTime,
                    role = new
                    {
                        name = StandartIdentityDataConstants.USER_ROLE,
                        headerMenuElements = menuElemsForRole,
                        routes = allRoutes
                    }
                });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login loginInfo, [FromServices]IAntiforgery antiforgery)
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

            var userToken = AccountControllerHelper.GenerateJwtToken(user, role[0]);
            /* var identity = new ClaimsIdentity(userToken.claims);
            var principal = new ClaimsPrincipal(identity);
            //привязывем пользователя к текущему контексту
            HttpContext.User = principal;

            //устанавливаем куки на валидацию от CSRF атаки
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);

            HttpContext.Response.Cookies.Append("RequestVerificationToken", tokens.RequestToken,
                new CookieOptions()
                {
                    HttpOnly = false
                });     
            */
            //отбор роутов для данной роли
            var allRoutes = await AccountControllerHelper.GetRoutesForRole(role[0], _routeRepository);
            //получение пунктов меню для пользователя
            var menuElemsForRole = _menuRepository.GetMenuElementsForRole(role[0]);
            var expireTime = Math.Round(DateTime.Now.ToUniversalTime().AddHours(AuthOptions.LIFETIME).Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds);
            return Ok(
                new
                {
                    name = user.Email,
                    token = userToken.token,
                    expireTimeToken = expireTime,
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
            HttpContext.Response.Cookies.Delete("RequestVerificationToken");
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