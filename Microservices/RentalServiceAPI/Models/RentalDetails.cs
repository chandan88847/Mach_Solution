﻿using System.ComponentModel.DataAnnotations;

namespace RentalServiceAPI.Models
{
    public class RentalDetails
    {
        [Key]
        public Guid RentalId { get; set; }

       
        public string OwnerUserId { get; set; }

        
        public string RenterUserId { get; set; }

        [StringLength(20)]
        public string VehicleRNumber { get; set; }

       
        public DateTime RentedDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Duration { get; set; }
       
        public double TotalAmount { get; set; }

        public string paymentId {  get; set; }
        
        public bool PaymentStatus { get; set; }

        [StringLength(100)]
        public string RentingLocation { get; set; }
    }
}
