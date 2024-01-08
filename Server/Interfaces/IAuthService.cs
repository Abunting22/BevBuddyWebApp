using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IAuthService
    {
        public Task<IActionResult> RegisterNewUserRequest(UserDto request);
        public Task<ActionResult> UserLoginRequestVerification(string username, string password);
    }
}
