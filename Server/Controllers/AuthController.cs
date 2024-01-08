using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authServices;

        public AuthController(IAuthService authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserDto request)
        {
            return await _authServices.RegisterNewUserRequest(request);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser(string username, string password)
        {
            return await _authServices.UserLoginRequestVerification(username, password);
        }
    }
}
