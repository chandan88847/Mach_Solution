using AuthenticationAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleFinderAPI.Models
{
    public class VehicleDetails
    {
        [Key]
        public string ItemId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId {  get; set; }
        public ApplicationUser ApplicationUser {  get; set; }        

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        [MaxLength(500)]
        public string ItemDescription { get; set; }

        [Required]
        [MaxLength(50)]
        public string ItemType { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Condition { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerHour { get; set; }

        [Required]
        [MaxLength(20)]
        public string VehicleRNumber { get; set; }

        [MaxLength(100)]
        public string Insurance { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }
    }
}
