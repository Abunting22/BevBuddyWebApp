using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IBetService
    {
        public Task<IActionResult> CreateNewBetRequest(BetDto request);

        public Task<IActionResult> UpdateBetRequest(BetDto request);

        public Task<Bet> ReturnAllBetsRequest(User user);

        public Task<Bet> ReturnSingleBetRequest(BetDto request);

        public Task<IActionResult> DeleteBetRequest(BetDto request);
    }
}
