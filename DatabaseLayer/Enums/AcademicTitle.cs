using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Enums
{
    public enum AcademicTitle : short
    {
        [Display(Name = "Студент(ка)")] Student = 0,
        [Display(Name = "Доцент")] AssistantProfessor = 1,
        [Display(Name = "Інше")] Other = 2
    }
}