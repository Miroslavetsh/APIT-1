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

        public Conference Conference { get; set; }
    }
}