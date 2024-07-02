namespace AuthenticationAPI.DTO
{
    public class LoginResponseDTO
    {
        public string ApplicationUserId {  get; set; }
        public string Email { get; set; }
        public string Token {  get; set; }
    }
}
