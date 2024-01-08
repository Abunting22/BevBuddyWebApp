using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BevBuddyWebApp.Server.Services
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _betRepository;

        public BetService(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        private Bet TransferBetDtoIntoBet(BetDto request)
        {
            Bet bet = new()
            {
                BetID = 0,
                UserID = request.UserID,
                Bettor = request.Bettor,
                Wager = request.Wager,
                Description = request.Description,
                WagerDate = request.WagerDate
            };

            return bet;
        }

        public async Task<IActionResult> CreateNewBetRequest(BetDto request)
        {
            var bet = TransferBetDtoIntoBet(request);

            return await _betRepository.CreateNewBet(bet);
        }

        public async Task<IActionResult> UpdateBetRequest(BetDto request)
        {
            var bet = TransferBetDtoIntoBet(request);

            return await _betRepository.UpdateBetByBetID(bet);
        }

        public async Task<Bet> ReturnAllBetsRequest(User user)
        {
            return await _betRepository.ReturnAllBetsByUserID(user);
        }

        public async Task<Bet> ReturnSingleBetRequest(BetDto request)
        {
            var bet = TransferBetDtoIntoBet(request);

            return await _betRepository.ReturnSingleBetByBetID(bet);
        }

        public async Task<IActionResult> DeleteBetRequest(BetDto request)
        {
            return await _betRepository.DeleteBetByBetID(request);
        }
    }
}
