using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Server.Services;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevBuddyWebApp.Server.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepositoryStub = new Mock<IUserRepository>().Object;

        [Fact]
        public async Task GetUserInfo_Returns_User_When_GetUserByUsernameControllerCallsMethod()
        {
            //Arrange:
            const string username = "testUsername";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(x => x.ReturnSingleUser(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<User>());
            var sut = new UserService(userRepositoryMock.Object);

            //Act:
            var actual = await sut.GetUserInfo(username);

            //Assert:
            userRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateUser_Returns_IActionResult_When_DependenciesSucceed()
        {
            //Arrange:
            const string password = "Foobar1!";
            var user = new User()
            {
                Username = "testUsername",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.bar@mail.com"
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(x => x.UpdateUserInfo(It.IsAny<User>()))
                .ReturnsAsync(new OkResult());
            var userDto = new UserDto();

            var sut = new UserService(userRepositoryMock.Object);

            //Act:
            var actual = await sut.UpdateUser(userDto);

            //Assert:
            userRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteUser_Returns_IActionResult_When_DependenciesSucceed()
        {
            //Arrange:
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(x => x.DeleteUser(It.IsAny<UserDto>()))
                .ReturnsAsync(new OkResult());
            var userDto = new UserDto();

            var sut = new UserService(userRepositoryMock.Object);

            //Act:
            var actual = await sut.DeleteUser(userDto);

            //Assert:
            userRepositoryMock.VerifyAll();
        }
    }
}
