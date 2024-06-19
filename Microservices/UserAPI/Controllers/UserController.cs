using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Model;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/UserProfile
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserProfiles()
        {
            var profiles = await _userService.GetAllUserProfilesAsync();
            return Ok(profiles);
        }

        // PUT: api/UserProfile
        [HttpPut("update")]
        public async Task<ActionResult<User>> UpdateUserProfile(User userProfile)
        {
            var updatedProfile = await _userService.UpdateUserProfileAsync(userProfile);
            if (updatedProfile == null)
            {
                return NotFound();
            }
            return Ok(updatedProfile);
        }

        // DELETE: api/UserProfile/{id}
        [HttpDelete("deleteuser/{id}")]
        public async Task<ActionResult> DeleteUserProfile(int id)
        {
            var result = await _userService.DeleteUserProfileAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
