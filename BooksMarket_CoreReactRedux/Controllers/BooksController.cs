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
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = 
          StandartIdentityDataConstants.ADMIN_ROLE + "," 
        + StandartIdentityDataConstants.STOREKEEPER_ROLE + "," 
        + StandartIdentityDataConstants.USER_ROLE)] 
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _booksRepository.GetAllBooks());
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksByFilter(string searchExpr, int page, [FromServices] IGenresRepository genresRep) => 
            Ok(await _booksRepository.GetBooksByFilter(searchExpr, (await genresRep.GetAllGenres()).ToList()));

        [HttpGet]
        public async Task<IActionResult> GetBooksByFilterInSomeGenre(string filter, string genre) => 
            Ok(await _booksRepository.GetBooksByFilterInSomeGenre(filter, genre));

        [HttpGet]
        public async Task<IActionResult> GetBooksByGenre(string genreTitle) => 
            Ok(await _booksRepository.GetBooksByGenre(genreTitle));

        [HttpPost]
        [Authorize(Roles = StandartIdentityDataConstants.ADMIN_ROLE + ","+ StandartIdentityDataConstants.STOREKEEPER_ROLE)]
        public async Task<IActionResult> AddBook([FromBody] RequestModels.Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var convertToDbObjectBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                DateCreating = new DateTime(Int32.Parse(book.DateCreating.Split(new char[] { '.' })[2]), 1, 1),
                GenreId = book.GenreId,
                Count = book.Count
            };
            await _booksRepository.AddElem(convertToDbObjectBook);
            return Ok();
        }
    }
}