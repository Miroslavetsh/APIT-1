using System.ComponentModel.DataAnnotations;
using DatabaseLayer;
using DatabaseLayer.Enums;

namespace BusinessLayer.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required] public string FirstName { get; set; }

        [Required(ErrorMessage = MSG.OnRequired)]
        public string LastName { get; set; }

        [Required(ErrorMessage = MSG.OnRequired)]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = MSG.OnRequired)]
        [Display(Name = "Місце навчання/роботи")]
        public string WorkingFor { get; set; }

        [Display(Name = "Научна ступінь")] public ScienceDegree ScienceDegree { get; set; }
        public string AltScienceDegree { get; set; }


        [Display(Name = "Академічна посада")] public AcademicTitle AcademicTitle { get; set; }
        public string AltAcademicTitle { get; set; }

        [Display(Name = "Форма участі")] public ParticipationForm ParticipationForm { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}