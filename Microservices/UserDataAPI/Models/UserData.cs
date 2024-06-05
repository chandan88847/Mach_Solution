using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserDataAPI.Models
{
    public class UserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {  get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
       
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        public string? UserMobile {  get; set; }

        public string? LicenceNumber {  get; set; }

        public bool IsHavingLicence=> !string.IsNullOrWhiteSpace(LicenceNumber);

        [Required]
        public string? UserGender { get; set; }

    }
}
