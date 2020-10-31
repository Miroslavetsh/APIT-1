using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class ConferenceParticipant
    {
        public Guid Id { get; set; }
        [Required] public string UserId { get; set; }

        [Required] public Conference Conference { get; set; }
    }

    public class ConferenceAdmin
    {
        public Guid Id { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public Conference Conference { get; set; }


        public bool CanAddAdmins { get; set; }
        public bool CanEditContent { get; set; }
        public bool CanSendMailing { get; set; }
    }

    public class ConferenceImage
    {
        public Guid Id { get; set; }
        [Required] public string ImagePath { get; set; }

        [Required] public Conference Conference { get; set; }
    }
}