using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class LoginViewModel
    {
        [Required]
        // [UIHint("email")]
        [Display(Name = "Email")] // It is better to use the Localization and the Resource files.
        public string Email { get; set; }

        [Required]
        // [UIHint("password")]
        [Display(Name = "Password")] // It is better to use the Localization and the Resource files.
        public string Password { get; set; }

        [Display(Name = "Remember me")] // It is better to use the Localization and the Resource files.
        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}