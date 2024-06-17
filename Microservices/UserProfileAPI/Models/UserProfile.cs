using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserProfileAPI.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        //public byte[] UserPhoto { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int KYCStatus { get; set; }
        public string DrivingLicenseId { get; set; }
        public string Security_Question { get; set; }
        public string Security_Answer { get; set; }
    }
}
