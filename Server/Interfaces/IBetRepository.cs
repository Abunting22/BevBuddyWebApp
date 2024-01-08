using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IBetRepository
    {
        public Task<IActionResult> CreateNewBet(Bet bet);

        public Task<IActionResult> UpdateBetByBetID(Bet bet);

        public Task<Bet> ReturnAllBetsByUserID(User user);

        public Task<Bet> ReturnSingleBetByBetID(Bet bet);

        public Task<IActionResult> DeleteBetByBetID(BetDto request);
    }
}
