using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Dapper;

namespace BevBuddyWebApp.Server.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly IBaseRepository _baseRepository;
        
        public BetRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IResult> CreateNewBet(Bet bet)
        {
            const string sql = $"""
                INSET INTO Bets (UserID, Bettor, Wager, Description, WagerDate)
                VALUES (@UserID, @Bettor, @Wager, @Description, @WagerDate)
                """;

            using var connection = _baseRepository.Connect();
            await connection.QueryAsync(sql, bet);

            return Results.Ok();
        }

        public async Task<IResult> UpdateBetByBetID(Bet bet)
        {
            const string sql = $"""
                UPDATE Bets
                SET Bettor = @Bettor, Wager = @Wager, Description = @Description
                WHERE BetID = @BetID
                """;

            using var connection = _baseRepository.Connect();
            await connection.QueryAsync<Bet>(sql, bet);

            return Results.Ok();
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

        public async Task<IResult> DeleteBetByBetID(BetDto request)
        {
            const string sql = $"""
                DELETE Bet FROM Bets
                WHERE BetID = @BetID
                """;

            using var connection = _baseRepository.Connect();
            await connection.ExecuteAsync(sql, request);

            return Results.Ok();
        }
    }
}