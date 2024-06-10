using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProfileAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UserProfileAPI.Controllers
{
    [Route("api/userprofile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UserProfileController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet("getusers")]
        public ActionResult<IEnumerable<UserProfileModel>> GetUsers()
        {
            List<UserProfileModel> userProfileModels = _userDbContext.Users.ToList();
            return userProfileModels;
        }

        [HttpGet("getusersbyid/{id}")]
        public ActionResult<UserProfileModel> GetUsersById([FromBody] int id)
        {
            UserProfileModel userProfileModel = _userDbContext.Users.Find(id);
            return userProfileModel;
        }

        [HttpPost("adduser")]
        public ActionResult AddUser([FromBody] UserProfileModel userProfileModel)
        {
            _userDbContext.Add(userProfileModel);
            _userDbContext.SaveChanges();
            return Ok("User Added Successfully");
        }

        [HttpPut("updateuser")]
        public ActionResult UpdateUser([FromBody] UserProfileModel userProfileModel)
        {
            _userDbContext.Update(userProfileModel);
            _userDbContext.SaveChanges();
            return Ok("User Updated Successfully");
        }

        [HttpDelete("deleteuser")]
        public ActionResult DeleteUser([FromBody] int id)
        {
            _userDbContext.Remove(id);
            _userDbContext.SaveChanges();
            return Ok("User Deleted Successfully");
        }
    }
}
