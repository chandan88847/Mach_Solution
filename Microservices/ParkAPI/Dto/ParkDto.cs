namespace ParkAPI.Dto
{
    public class ParkDto
    {
        public string ApplicationUserId { get; set; }

        public string VehicleDescription { get; set; }

        public string Location { get; set; }

        public string VehicleNumber { get; set; }

        public string Address { get; set; }

        public double AvailableHours { get; set; }

        public double PricePerHour {  get; set; }
        public DateTime ExpectedReturnTime { get; set; }

        public IFormFile VehicleImage { get; set; }
        
    }
}
