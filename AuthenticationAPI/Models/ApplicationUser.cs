using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public String Name {  get; set; }
    }
}
