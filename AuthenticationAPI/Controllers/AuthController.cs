using AuthenticationAPI.Data;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using AuthenticationAPI.Services;
using AuthenticationAPI.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _apiResponse;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole>  _roleManager;
        private readonly ApplicationUserService _applicationUserService;
        private string secret_key;
        private int validtime;

        public AuthController(ApplicationDbContext db,IConfiguration configuration, RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser>userManager, ApplicationUserService applicationUserService)
        {
            _db = db;
            secret_key = configuration.GetValue<string>("ApiSettings:SecretKey");
           // validtime = configuration.GetValue<int>("ApiSettings",validtime);
            _apiResponse = new ApiResponse();
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationUserService = applicationUserService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            ApplicationUser userFromDb = _db.ApplicationUsers.FirstOrDefault
               (u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, loginRequestDTO.Password);
            if (!isValid)
            {
                _apiResponse.httpstatuscode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Errors.Add("user name or password is incorrect");
                return BadRequest(_apiResponse);

            }
            var roles= await _userManager.GetRolesAsync(userFromDb);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key=Encoding.ASCII.GetBytes(secret_key);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName",userFromDb.Name),
                    new Claim("id",userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email,userFromDb.Email.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())
                }),
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)  
            };
          
            SecurityToken token=tokenHandler.CreateToken(securityTokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Email=userFromDb.Email,
                ApplicationUserId=userFromDb.Id,
                Token= tokenHandler.WriteToken(token)
            };

            if(loginResponseDTO.Email==null)
            {
                _apiResponse.httpstatuscode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Errors.Add("user name or password is incorrect");
                return BadRequest(_apiResponse);
            }

            _apiResponse.httpstatuscode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result= loginResponseDTO;
            return Ok(_apiResponse);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {
            ApplicationUser userFromDb=_db.ApplicationUsers.FirstOrDefault
                (u=>u.UserName.ToLower()==registerDTO.UserName.ToLower());

            if(userFromDb!=null) {
                _apiResponse.httpstatuscode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Errors.Add("UserName Already Exits");
                return BadRequest(_apiResponse);
            }

            ApplicationUser applicationUser=new ApplicationUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                
            };

            try
            {

                var result = await _userManager.CreateAsync(applicationUser, registerDTO.Password);

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(Constants.Role_Admin).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Constants.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(Constants.Role_User));

                    }
                    if (registerDTO.Role?.ToLower() == Constants.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(applicationUser, Constants.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(applicationUser, Constants.Role_Admin);
                    }
                    var user = _applicationUserService.GetUserIdByUserName(registerDTO.UserName);
                    bool flag=await _applicationUserService.ApplicationUserEntityAsync(user.Result.ToString());
                    _apiResponse.httpstatuscode = HttpStatusCode.OK;
                    _apiResponse.IsSuccess = true;
                    return Ok(_apiResponse);

                }
            }
            catch (Exception ex)
            {
               
                
            }
            _apiResponse.httpstatuscode = HttpStatusCode.BadRequest;
            _apiResponse.IsSuccess = false;
            _apiResponse.Errors.Add("Error while getting registered");
            return BadRequest(_apiResponse);

        }

        [HttpGet("GetUserProfileByUserID/{UserId}")]
        public async Task<ActionResult<UsermProfileDTO>> GetUserProfileByApplicationID(string UserId)
        {
            var profile = await _applicationUserService.GetUserByUserId(UserId);
            return Ok(profile);
        }

    }
}
