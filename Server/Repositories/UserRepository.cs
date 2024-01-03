using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository _baseRepository;

        public UserRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<User> ReturnSingleUser(string username)
        {
            const string sql = $"""
                SELECT UserID, Username, FirstName, LastName, Email
                FROM Users
                WHERE Username = @Username
                """;

            using var connection = _baseRepository.Connect();
            User user = await connection.QueryFirstOrDefaultAsync(sql, username)
                        ?? throw new ArgumentException("User not found");

            return user;
        }

        public async Task<IActionResult> UpdateUserInfo(User user)
        {
            const string sql = $"""
                UPDATE Users
                SET Username = @Username, Password = @PasswordHash, FirstName = @FirstName, LastName = @LastName, Email = @Email
                WHERE Username = @Username
                """;

            using var connection = _baseRepository.Connect();
            await connection.QueryAsync<User>(sql, user);

            return new OkResult();
        }

        public async Task<IActionResult> DeleteUser(UserDto request)
        {
            const string sql = $"""
                DELETE FROM Users
                WHERE Username = @Username AND Password = @Password
                """;

            using var connection = _baseRepository.Connect();
            await connection.ExecuteAsync(sql, request);

            return new OkResult();
        }
    }
}
