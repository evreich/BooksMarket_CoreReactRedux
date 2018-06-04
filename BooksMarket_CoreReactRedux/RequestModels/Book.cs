using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.RequestModels
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в поле Title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в поле DateCreating.")]
        public string DateCreating { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в поле Author.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Ошибка! Выберите жанр.")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в поле Count.")]
        public int Count { get; set; }
    }
}
