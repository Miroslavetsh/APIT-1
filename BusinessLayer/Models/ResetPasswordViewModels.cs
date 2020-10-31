using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class EmailViewModel
    {
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required] public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}