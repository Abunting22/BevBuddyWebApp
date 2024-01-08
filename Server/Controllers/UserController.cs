using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetUserByUsername")]
        public async Task<User> GetUserByUsername(string username)
        {
            return await _userServices.GetUserInfo(username);
        }

        [HttpPost("UpdateUserByUsername")]
        public async Task<IActionResult> UpdateUserByUsername(UserDto request)
        {
            return await _userServices.UpdateUser(request);
        }

        [HttpDelete("DeleteUserByUsernameAndPassword")]
        public async Task<IActionResult> DeleteUserByUsernameAndPassword(UserDto request)
        {
            return await _userServices.DeleteUser(request);
        }
    }
}
