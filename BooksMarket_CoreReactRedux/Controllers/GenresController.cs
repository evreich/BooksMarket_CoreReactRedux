using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksMarket_CoreReactRedux.DbRepositories;
using BooksMarket_CoreReactRedux.EF.SeedDbHelpers;
using BooksMarket_CoreReactRedux.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksMarket_CoreReactRedux.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles =
          StandartIdentityDataConstants.ADMIN_ROLE + ","
        + StandartIdentityDataConstants.STOREKEEPER_ROLE)]
    public class GenresController : Controller
    {
        private readonly IGenresRepository _genresRepository;

        public GenresController(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await _genresRepository.GetAllGenres()); 
        }
     }
}