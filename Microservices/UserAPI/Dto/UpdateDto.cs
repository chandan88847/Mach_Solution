namespace UserAPI.Dto
{
    public class UpdateDto
    {
        public string ApplicationUserId { get; set; }  // Foreign key
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? DrivingLicenseId { get; set; }
    }
}
