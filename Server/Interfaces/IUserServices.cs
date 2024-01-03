using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IUserServices
    {
        public Task<User> GetUserInfo(string username);

        public Task<IActionResult> UpdateUser(UserDto request);

        public Task<IActionResult> DeleteUser(UserDto request);
    }
}
