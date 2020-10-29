using System;

namespace DatabaseLayer.Entities
{
    public class ConferenceParticipant
    {
        public string Id { get; set; }

        public Conference Conference { get; set; }
    }

    public class ConferenceAdmin
    {
        public string Id { get; set; }
        public AdminPermissions Permissions { get; set; }

        public Conference Conference { get; set; }
    }

    public class ConferenceImage
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }

        public Conference Conference { get; set; }
    }

    public class AdminPermissions
    {
        public Guid Id { get; set; }

        public bool CanAddAdmins { get; set; }
        public bool CanEditContent { get; set; }
        public bool CanSendMailing { get; set; }

        public User User { get; set; }
    }
}