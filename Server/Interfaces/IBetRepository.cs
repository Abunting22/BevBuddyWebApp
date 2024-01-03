using BevBuddyWebApp.Shared.Models;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IBetRepository
    {
        public Task<IResult> CreateNewBet(Bet bet);

        public Task<IResult> UpdateBetByBetID(Bet bet);

        public Task<Bet> ReturnAllBetsByUserID(User user);

        public Task<Bet> ReturnSingleBetByBetID(Bet bet);

        public Task<IResult> DeleteBetByBetID(BetDto request);
    }
}
