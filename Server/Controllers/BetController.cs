using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Controllers
{
    public class BetController
    {
        private readonly IBetService _betService;

        public BetController(IBetService betService)
        {
            _betService = betService;
        }

        [HttpPost("CreateBet")]
        public async Task<IActionResult> CreateNewBet(BetDto request)
        {
            return await _betService.CreateNewBetRequest(request);
        }

        [HttpPost("UpdateBet")]
        public async Task<IActionResult> UpdateBet(BetDto request)
        {
            return await _betService.UpdateBetRequest(request);
        }

        [HttpGet("GetAllBets")]
        public async Task<Bet> GetAllBets(User user)
        {
            return await _betService.ReturnAllBetsRequest(user);
        }

        [HttpGet("GetSingleBet")]
        public async Task<Bet> GetSingleBet(BetDto request)
        {
            return await _betService.ReturnSingleBetRequest(request);
        }

        [HttpDelete("DeleteBet")]
        public async Task<IActionResult> DeleteBet(BetDto request)
        {
            return await _betService.DeleteBetRequest(request);
        }
    }
}
