using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IAuthRepository
    {
        public Task<IActionResult> RegisterNewUser(User user);
        public Task<User> Login(UserDto request);
    }
}
