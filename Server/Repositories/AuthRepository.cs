using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IBaseRepository _baseRepository;

        public AuthRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IActionResult> RegisterNewUser(User user)
        {
            const string sql = $"""
                INSERT INTO Users (Username, Password, FirstName, LastName, Email)
                VALUES (@Username, @PasswordHash, @FirstName, @LastName, @Email)
                """;

            using var connection = _baseRepository.Connect();
            await connection.QueryAsync(sql, user);

            return new OkResult();
        }

        public async Task<User> Login(UserDto request)
        {
            const string sql = $"""
                SELECT Username, Password
                FROM Users
                WHERE Username = @Username
                """;

            using var connection = _baseRepository.Connect();
            User user = await connection.QueryFirstOrDefaultAsync(sql, request);

            return new User();
        }
    }
}
