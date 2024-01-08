using BevBuddyWebApp.Server.Controllers;
using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevBuddyWebApp.Server.Tests.Controllers
{
    public class BetControllerTests
    {
        [Fact]
        public async Task CreateNewBet_Returns_IActionResult_When_DependenciesSucceed()
        {
            var betServiceMock = new Mock<IBetService>();
            betServiceMock
                .Setup(x => x.CreateNewBetRequest(It.IsAny<BetDto>()))
                .ReturnsAsync(It.IsAny<IActionResult>());
            var betDto = new BetDto();

            var sut = new BetController(betServiceMock.Object);

            var actual = await sut.CreateNewBet(betDto);

            betServiceMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateBet_Returns_IActionResult_When_DependenciesSucceed()
        {
            var betServiceMock = new Mock<IBetService>();
            betServiceMock
                .Setup(x => x.UpdateBetRequest(It.IsAny<BetDto>()))
                .ReturnsAsync(It.IsAny<IActionResult>());
            var betDto = new BetDto();

            var sut = new BetController(betServiceMock.Object);

            var actual = await sut.UpdateBet(betDto);

            betServiceMock.VerifyAll();
        }

        [Fact]
        public async Task GetAllBets_Returns_Bet_When_DependenciesSucceed()
        {
            var betServiceMock = new Mock<IBetService>();
            betServiceMock
                .Setup(x => x.ReturnAllBetsRequest(It.IsAny<User>()))
                .ReturnsAsync(It.IsAny<Bet>());
            var user = new User();

            var sut = new BetController(betServiceMock.Object);

            var actual = await sut.GetAllBets(user);

            betServiceMock.VerifyAll();
        }

        [Fact]
        public async Task GetSinlgeBet_Returns_Bet_When_DependenciesSucceed()
        {
            var betServiceMock = new Mock<IBetService>();
            betServiceMock
                .Setup(x => x.ReturnSingleBetRequest(It.IsAny<BetDto>()))
                .ReturnsAsync(It.IsAny<Bet>());
            var betDto = new BetDto();

            var sut = new BetController(betServiceMock.Object);

            var actual = await sut.GetSingleBet(betDto);

            betServiceMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteBet_Returns_IActionResult_When_DependenciesSucceed()
        {
            var betServiceMock = new Mock<IBetService>();
            betServiceMock
                .Setup(x => x.DeleteBetRequest(It.IsAny<BetDto>()))
                .ReturnsAsync(It.IsAny<IActionResult>());
            var betDto = new BetDto();

            var sut = new BetController(betServiceMock.Object);

            var actual = await sut.DeleteBet(betDto);

            betServiceMock.VerifyAll();
        }
    }
}
