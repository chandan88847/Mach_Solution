﻿using System.ComponentModel.DataAnnotations;

namespace RentalServiceAPI.Models
{
    public class RentalDetails
    {
        [Key]
        public Guid RentalId { get; set; }

        [Required]
        public string OwnerUserId { get; set; }

        [Required]
        public string RenterUserId { get; set; }

        [Required]
        [StringLength(20)]
        public string VehicleRNumber { get; set; }

        [Required]
        public DateTime RentedDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Duration { get; set; }
       
        [DataType(DataType.Currency)]
        public string TotalAmount { get; set; }
        
        public bool PaymentStatus { get; set; }

        [StringLength(100)]
        public string RentingLocation { get; set; }
    }
}
