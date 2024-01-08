using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserInfo(string username)
        {
            User user = await _userRepository.ReturnSingleUser(username);

            return user;
        }

        public async Task<IActionResult> UpdateUser(UserDto request)
        {
            User user = new()
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            return await _userRepository.UpdateUserInfo(user);
        }

        public async Task<IActionResult> DeleteUser(UserDto request)
        {
            await _userRepository.DeleteUser(request);

            return new OkResult();
        }
    }
}
