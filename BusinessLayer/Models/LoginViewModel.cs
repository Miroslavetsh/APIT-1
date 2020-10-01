using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class LoginViewModel
    {
        [Required]
        // [UIHint("email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        // [UIHint("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}