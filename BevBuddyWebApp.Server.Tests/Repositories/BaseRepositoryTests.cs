using BevBuddyWebApp.Server.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BevBuddyWebApp.Server.Tests.Repositories
{
    public class BaseRepositoryTests
    {            
        [Fact]
        public void Connect_Returns_SqlConnection_When_DependenciesSucceed()
        {
            var configSecMock = new Mock<IConfigurationSection>();
            configSecMock
                .SetupGet(m => m[It.Is<string>(s => s == "TestConnection")])
                .Returns("TestConnection");

            var configurationMock = new Mock<IConfiguration>();
            configurationMock
                .Setup(x => x.GetSection("ConnectionStrings"))
                .Returns(configSecMock.Object);

            var sut = new BaseRepository(configurationMock.Object);

            var actual = sut.Connect();

            Assert.NotNull(actual);
            Assert.IsType<SqlConnection>(actual);
        }
    }
}
