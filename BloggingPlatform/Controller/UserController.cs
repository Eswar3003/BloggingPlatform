using BloggingPlatform.Helper;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace BloggingPlatform.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBL _userBL;

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }


        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUserAsync(LoginDto loginDto)
        {
            var response = await _userBL.CreateUserAsync(loginDto);            
            return Ok(response);
        }


        [Authorize]
        [HttpGet("getuserdetails/{id}")]
        public async Task<IActionResult> GetUserDetailsAsyncById(int id)
        {
            UserDto response = new();
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            var userDetail = await _userBL.GetUserDetailsAsyncById(id);
            if (userDetail == null) return BadRequest(new { message = "User Id not exist" });
            else
            {
                if (userDetail.Id == loggedInUserDetail.Id || loggedInUserDetail.Id == 1) response = userDetail;
                else return BadRequest(new { message = "You are not authorized" });
            }
            return Ok(response);
        }


        [HttpGet("login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var response = await _userBL.LoginAsync(loginDto);
            if (response == null)
                return BadRequest(new { message = "Username is not exist" });

            return Ok(response);
        }

    }
}
