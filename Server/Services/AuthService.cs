using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtServices;
        private readonly string ErrorMessage = "Invalid Username or Password";

        public AuthService(IAuthRepository authRepository, IJwtService jwtServices)
        {
            _authRepository = authRepository;
            _jwtServices = jwtServices;
        }

        public async Task<IActionResult> RegisterNewUserRequest(UserDto request)
        {
            User user = new()
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            return await _authRepository.RegisterNewUser(user);
        }

        public async Task<ActionResult> UserLoginRequestVerification(string username, string password)
        {
            UserDto request = new()
            {
                Username = username,
                Password = password
            };

            User user = new() 
            {
                Username = username
            };

            var isValidLogin = await IsValidLogin(request);
            
            if (isValidLogin) 
            {
                var token = _jwtServices.GetJwt(user);

                return new OkObjectResult(token);
            }

            return new BadRequestObjectResult(ErrorMessage);
        }

        private async Task<bool> IsValidLogin(UserDto request)
        {
            User user = await _authRepository.Login(request);

            if (user != null && request.Username == user.Username &&
                BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return true;
            }

            return false;
        }
    }
}
