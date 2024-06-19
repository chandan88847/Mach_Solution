using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AuthenticationAPI.Models;

namespace UserAPI.Model
{
    public class User
    {
        [Key]
        public Guid UserProfileId { get; set; }

        public string ApplicationUserId { get; set; }  // Foreign key

        //public byte[] UserPhoto { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public int? KYCStatus { get; set; }
        public string? DrivingLicenseId { get; set; }
     
    }
}
