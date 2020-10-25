using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Identity;

namespace DatabaseLayer.Entities
{
    public class User : IdentityUser
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string WorkingFor { get; set; }
        public ScienceDegree ScienceDegree { get; set; }
        public AcademicTitle AcademicTitle { get; set; }
        public ParticipationForm ParticipationForm { get; set; }

        [Required] public string ProfileAddress { get; set; }

        [NotMapped] public string FullName => $"{LastName} {FirstName} {MiddleName}";
        public static bool operator ==(User a, User b) => a?.Id == b?.Id;
        public static bool operator !=(User a, User b) => !(a == b);

        public override int GetHashCode() => Id.GetHashCode();

        private bool Equals(User other) => other != null && Id == other.Id;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        public override string ToString() =>
            $"{LastName} {FirstName} {MiddleName} -> " +
            $"email({EmailConfirmed}):{Email} " +
            $"phone({PhoneNumberConfirmed}):{PhoneNumber}";
    }
}