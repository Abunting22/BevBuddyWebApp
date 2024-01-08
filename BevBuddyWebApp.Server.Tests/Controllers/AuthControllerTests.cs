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
    public class AuthControllerTests
    {
        [Fact]
        public async Task RegisterUser_Returns_IActionResult_When_DependenciesSucceed()
        {
            //Arrange:
            var authServicesMock = new Mock<IAuthService>();
            authServicesMock
                .Setup(x => x.RegisterNewUserRequest(It.IsAny<UserDto>()))
                .ReturnsAsync(It.IsAny<IActionResult>());
            var userDto = new UserDto();

            var sut = new AuthController(authServicesMock.Object);

            //Act:
            var actual = await sut.RegisterUser(userDto);

            //Assert:
            authServicesMock.VerifyAll();
        }

        [Fact]
        public async Task LoginUser_Returns_ActionResult_When_DependenciesSucceed()
        {
            const string username = "Foo";
            const string password = "Bar";

            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(x => x.UserLoginRequestVerification(username, password))
                .ReturnsAsync(It.IsAny<ActionResult>());

            var sut = new AuthController(authServiceMock.Object);

            var actual = await sut.LoginUser(username, password);

            authServiceMock.VerifyAll();
        }
    }
}
