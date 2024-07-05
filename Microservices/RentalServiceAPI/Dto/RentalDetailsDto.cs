using System.ComponentModel.DataAnnotations;

namespace RentalServiceAPI.Dto
{
    public class RentalDetailsDto
    {
        public string OwnerUserId { get; set; }


        public string RenterUserId { get; set; }

       
        public string VehicleRNumber { get; set; }


        public DateTime RentedDate { get; set; }

        public double Duration { get; set; }

        public double TotalAmount { get; set; }

        public string paymentId { get; set; }

        public string RentingLocation { get; set; }
    }
}
