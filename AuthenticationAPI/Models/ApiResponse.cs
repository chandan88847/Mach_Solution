using System.Net;

namespace AuthenticationAPI.Models
{
    public class ApiResponse
    {
        public ApiResponse() { 

            Errors = new List<string>();
        }
        public HttpStatusCode httpstatuscode {  get; set; }
        public bool IsSuccess { get; set; } = true;

        public List<string> Errors { get; set; }

        public object Result {  get; set; }
    }
}
