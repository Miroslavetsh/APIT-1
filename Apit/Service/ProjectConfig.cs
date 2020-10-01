namespace BusinessLayer
{
    public class ProjectConfig
    {
        public FeedbackConfig Feedback { get; set; }


        public class FeedbackConfig
        {
            public string PhoneNumber { get; set; }
            public string ShortPhone { get; set; }
            public string Email { get; set; }
        }
    }
}