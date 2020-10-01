using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Enums;

namespace BusinessLayer.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string WorkingFor { get; set; }
        public ScienceDegree ScienceDegree { get; set; }
        public AcademicTitle AcademicTitle { get; set; }
        public ParticipationForm ParticipationForm { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}