using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMarket_CoreReactRedux.RequestModels
{
    public class Registration
    {
        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w_.-]+@[\w-_]+\.\w{2,3}$", ErrorMessage = "Ошибка! Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [Display(Name = "ФИО")]
        [RegularExpression(@"[а-яА-Я\w]+ [а-яА-Я\w]+ [а-яА-Я\w]+$", ErrorMessage = "Ошибка! Некорректный формат ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Ошибка! Некорректный формат телефона")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Ошибка! Длина ключа должна быть >= 5")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ошибка! Введите данные в заданное поле.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
