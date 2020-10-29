using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Enums
{
    public enum ScienceDegree : short
    {
        [Display(Name = "Бакалавр")] Bachelor,
        [Display(Name = "Магістр")] Master,
        [Display(Name = "Аспірант")] Graduate,
        [Display(Name = "Кандидат наук")] Candidate,
        [Display(Name = "Доктор наук")] Doctor,
        [Display(Name = "Інше")] Other
    }
}