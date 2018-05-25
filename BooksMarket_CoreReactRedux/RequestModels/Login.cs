using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.RequestModels
{
    public class Login
    {
        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w_.-]+@[\w-_]+\.\w{2,3}$", ErrorMessage = "Ошибка! Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Ошибка! Длина пароля должна быть >= 5")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить ")]
        public bool RememberMe { get; set; }
    }
}
