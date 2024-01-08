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
    public class UserControllerTests
    {
        [Fact]
        public async Task GetUserByUsername_Returns_User_When_DependenciesSucceed()
        {
            const string username = "FooBar";
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.GetUserInfo(username))
                .ReturnsAsync(It.IsAny<User>());
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.GetUserByUsername(username);

            userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateUserByUsername_Returns_IActionResult_When_DependenciesSucceed()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.UpdateUser(It.IsAny<UserDto>()))
                .ReturnsAsync(It.IsAny<IActionResult>());
            var userDto = new UserDto();
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.UpdateUserByUsername(userDto);

            userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteUserByUsernameAndPassword_Returns_IActionResult_When_DependenciesSucceed()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.DeleteUser(It.IsAny<UserDto>()))
                .ReturnsAsync(It.IsAny<IActionResult>());
            var userDto = new UserDto();
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.DeleteUserByUsernameAndPassword(userDto);

            userServiceMock.VerifyAll();
        }
    }
}
