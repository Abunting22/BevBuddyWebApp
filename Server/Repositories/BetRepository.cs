using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly IBaseRepository _baseRepository;
        
        public BetRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IActionResult> CreateNewBet(Bet bet)
        {
            const string sql = $"""
                INSERT INTO Bets (UserID, Bettor, Wager, Description, WagerDate)
                VALUES (@UserID, @Bettor, @Wager, @Description, @WagerDate)
                """;

            using var connection = _baseRepository.Connect();
            await connection.ExecuteAsync(sql, bet);

            return new OkResult();
        }

        public async Task<IActionResult> UpdateBetByBetID(Bet bet)
        {
            const string sql = $"""
                UPDATE Bets
                SET Bettor = @Bettor, Wager = @Wager, Description = @Description
                WHERE BetID = @BetID
                """;

            using var connection = _baseRepository.Connect();
            await connection.QueryAsync<Bet>(sql, bet);

            return new OkResult();
        }

        public async Task<Bet> ReturnAllBetsByUserID(User user)
        {
            const string sql = $"""
                SELECT * FROM Bets
                WHERE UserID = @UserID
                """;

            using var connection = _baseRepository.Connect();
            Bet bets = await connection.QueryFirstOrDefaultAsync(sql, user)
                       ?? throw new ArgumentNullException();

            return bets;
        }

        public async Task<Bet> ReturnSingleBetByBetID(Bet bet)
        {
            const string sql = $"""
                SELECT Bet FROM Bets
                WHERE BetID = @BetID
                """;

            using var connection = _baseRepository.Connect();
            Bet returnBet = await connection.QueryFirstOrDefaultAsync(sql, bet)
                            ?? throw new ArgumentNullException();

            return returnBet;
        }

        public async Task<IActionResult> DeleteBetByBetID(BetDto request)
        {
            const string sql = $"""
                DELETE Bet FROM Bets
                WHERE BetID = @BetID
                """;

            using var connection = _baseRepository.Connect();
            await connection.ExecuteAsync(sql, request);

            return new OkResult();
        }
    }
}