using System.ComponentModel.DataAnnotations;
using System;

namespace RentalAPI.Models
{
    public class RentalDetails
    {
        [Key]
        public Guid RentalId { get; set; }

        public string OwnerUserId { get; set; }

        public string RenterUserId { get; set; }

        public string Vehicle_Number { get; set; }

        public DateTime RentedDate { get; set; }

        public double RentingTime { get; set; }

        public bool PaymentStatus { get; set; }

        public string PaymentId { get; set; }

        public string RentingLocation { get; set; }
    }
}