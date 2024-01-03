using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> ReturnSingleUser(string username);

        public Task<IActionResult> UpdateUserInfo(User user);

        public Task<IActionResult> DeleteUser(UserDto request);
    }
}
