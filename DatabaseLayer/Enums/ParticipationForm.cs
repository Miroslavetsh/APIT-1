using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Enums
{
    public enum ParticipationForm : short
    {
        [Display(Name = "Глядач")] Listener = 0,
        [Display(Name = "Подання тез")] ThesesPreparer = 1,
        [Display(Name = "Доповідач")] Speaker = 2,
    }
}