using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Server.Repositories;
using BevBuddyWebApp.Shared.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data;

namespace BevBuddyWebApp.Server.Tests.Repositories
{
    public class AuthRepositoryTests
    {
        private readonly IConfiguration configStub = new Mock<IConfiguration>().Object;

        [Fact]
        public async Task RegisterNewUser_Returns_IActionResult_When_DependenciesSucceed()
        {
            //Arrange:
            const string password = "Foobar1!";
            var testConnectionString = configStub.GetSection("TestConnection");

            User user = new()
            {
                UserID = 1,
                Username = "FooBar",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.bar@mail.com"
            };
            var baseRepositoryMock = new Mock<IBaseRepository>();
            baseRepositoryMock
                .Setup(x => x.Connect())
                .Returns(It.IsIn<IDbConnection>(new SqlConnection()));

            var sut = new AuthRepository(baseRepositoryMock.Object);

            //Act:
            var actual = await sut.RegisterNewUser(user);

            //Assert:
            Assert.IsType<OkResult>(actual);
        }
    }
}
