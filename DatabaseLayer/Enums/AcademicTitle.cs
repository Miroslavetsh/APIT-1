using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Enums
{
    public enum AcademicTitle : short
    {
        Normal = 0,
        BestOfTheBest,
        [Display(Name = "Vety Good Person")] VeryGoodPerson = 254
    }
}