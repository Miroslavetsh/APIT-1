using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class Conference
    {
        public Guid Id { get; set; }

        public string UniqueAddress { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public ICollection<ConferenceParticipant> Participants { get; set; }
        public ICollection<ConferenceAdmin> Admins { get; set; }
        public ICollection<Article> Articles { get; set; }


        [DataType(DataType.Date)] public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)] public DateTime DateLastModified { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateStart { get; set; }
        [DataType(DataType.Duration)] public DateTime Duration { get; set; }


        public Conference()
        {
            Participants = new List<ConferenceParticipant>();
            Admins = new List<ConferenceAdmin>();
            Articles = new List<Article>();
        }


        public static bool operator ==(Conference a, Conference b) => a?.Id == b?.Id;
        public static bool operator !=(Conference a, Conference b) => !(a == b);

        public override int GetHashCode() => Id.GetHashCode();

        private bool Equals(Conference other) => other != null && Id == other.Id;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }
    }
}