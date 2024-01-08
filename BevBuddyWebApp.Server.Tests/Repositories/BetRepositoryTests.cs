using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Server.Repositories;
using BevBuddyWebApp.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevBuddyWebApp.Server.Tests.Repositories
{
    public class BetRepositoryTests
    {
        [Fact]
        public async Task CreateNewBet_Returns_IActionResult_When_DependenciesSucceed()
        {
            var bet = new Bet()
            {
                BetID = 1,
                UserID = 1,
                Bettor = "FooBar",
                Wager = 1,
                Description = "Test bet",
                WagerDate = DateTime.Now
            };
            var configMockSec = new Mock<IConfigurationSection>();
            configMockSec
                .SetupGet(m => m[It.Is<string>(s => s == "TestConnection")])
                .Returns("TestConnection");

            var configMock = new Mock<IConfiguration>();
            configMock
                .Setup(x => x.GetSection("ConnectionStrings"))
                .Returns(configMockSec.Object);

            var mockBaseRepository = new Mock<IBaseRepository>();
            mockBaseRepository
                .Setup(x => x.Connect())
                .Returns(configMock.Object);

            var sut = new BetRepository(mockBaseRepository.Object);

            var actual = await sut.CreateNewBet(bet);

            mockBaseRepository.VerifyAll();
        }
    }
}
