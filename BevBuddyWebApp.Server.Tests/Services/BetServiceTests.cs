using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Server.Services;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BevBuddyWebApp.Server.Tests.Services
{
    public class BetServiceTests
    {
        [Fact]
        public async Task CreateNewBetRequest_Returns_IActionResult_When_DependenciesSucceed()
        {
            //Arrange:
            var betRepositoryMock = new Mock<IBetRepository>();
            betRepositoryMock
                .Setup(x => x.CreateNewBet(It.IsAny<Bet>()))
                .ReturnsAsync(new OkResult());
            var betDto = new BetDto();

            var sut = new BetService(betRepositoryMock.Object);

            //Act:
            var actual = await sut.CreateNewBetRequest(betDto);

            //Assert:
            betRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateBetRequest_Returns_IActionResult_When_DependenciesSucceed()
        {
            var betRespositoryMock = new Mock<IBetRepository>();
            betRespositoryMock
                .Setup(x => x.UpdateBetByBetID(It.IsAny<Bet>()))
                .ReturnsAsync(new OkResult());
            var betDto = new BetDto();

            var sut = new BetService(betRespositoryMock.Object);

            var actual = await sut.UpdateBetRequest(betDto);

            betRespositoryMock.VerifyAll();
        }

        [Fact]
        public async Task ReturnAllBetsRequest_Returns_Bet_When_DependenciesSucceed()
        {
            var betRepositoryMock = new Mock<IBetRepository>();
            betRepositoryMock
                .Setup(x => x.ReturnAllBetsByUserID(It.IsAny<User>()))
                .ReturnsAsync(It.IsAny<Bet>());
            var user = new User();

            var sut = new BetService(betRepositoryMock.Object);

            var actual = await sut.ReturnAllBetsRequest(user);

            betRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task ReturnSingleBetRequest_Returns_Bet_When_DependenciesSucceed()
        {
            var betRepositoryMock = new Mock<IBetRepository>();
            betRepositoryMock
                .Setup(x => x.ReturnSingleBetByBetID(It.IsAny<Bet>()))
                .ReturnsAsync(It.IsAny<Bet>());
            var betDto = new BetDto();

            var sut = new BetService(betRepositoryMock.Object);

            var actual = await sut.ReturnSingleBetRequest(betDto);

            betRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteBetRequest_Returns_IActionResult_When_DependenciesSucceed()
        {
            var betRepositoryMock = new Mock<IBetRepository>();
            betRepositoryMock
                .Setup(x => x.DeleteBetByBetID(It.IsAny<BetDto>()))
                .ReturnsAsync(new OkResult());
            var betDto = new BetDto();

            var sut = new BetService(betRepositoryMock.Object);

            var actual = await sut.DeleteBetRequest(betDto);

            betRepositoryMock.VerifyAll();
        }
    }
}
