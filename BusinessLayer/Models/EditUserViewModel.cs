using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Models
{
    public class EditUserViewModel
    {
        public IFormFile ProfilePhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }


        public string WorkingFor { get; set; }

        public string AltScienceDegree { get; set; }
        public ScienceDegree ScienceDegree { get; set; }


        public string AltAcademicTitle { get; set; }
        public AcademicTitle AcademicTitle { get; set; }

        public ParticipationForm ParticipationForm { get; set; }


        // public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }
        public string ResultMessage { get; set; }
    }
}