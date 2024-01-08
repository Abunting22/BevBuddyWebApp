using BevBuddyWebApp.Server.Interfaces;
using BevBuddyWebApp.Server.Services;
using BevBuddyWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BevBuddyWebApp.Server.Tests.Services
{
    public class AuthServiceTests
    {
        // Unit Test Naming: SystemUnderTest_Returns_ReturnType_When_Condition
        // 3As: Arrange, Act, Assert
        // Mock: Mocks have a "Setup"
        // Stub: Stubs don't have a "Setup"
        // Change IResult to IActionResult => see approved answer: https://stackoverflow.com/questions/41292919/unit-testing-controller-methods-which-return-iactionresult

        private readonly IAuthRepository _authRepositoryStub = new Mock<IAuthRepository>().Object;
        private readonly IJwtService _jwtServicesStub = new Mock<IJwtService>().Object;


        [Fact]
        public async Task RegisterNewUserRequest_Returns_IActionResult_When_DependenciesSucceed()
        {
            // Arrange:
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock
                .Setup(x => x.RegisterNewUser(It.IsAny<User>()))
                .ReturnsAsync(It.IsAny<ActionResult>());   
            var userDto = new UserDto();
            var sut = new AuthService(authRepositoryMock.Object, _jwtServicesStub);

            // Act: 
            var actual = await sut.RegisterNewUserRequest(userDto);

            // Assert:
            authRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UserLoginRequestVerification_Returns_ActionResult_When_VerifyUserRequestIsValid()
        {
            // Arrange:
            const string username = "alex";
            const string password = "Password123$";
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                UserID = 1,
                Username = username,
                PasswordHash = passwordHash,
                FirstName = "Alex",
                LastName = "Bunting",
                Email = "alex.bunting@mail.com"
            };

            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock
                .Setup(x => x.Login(It.IsAny<UserDto>()))
                .ReturnsAsync(user);

            var jwtServicesMock = new Mock<IJwtService>();
            jwtServicesMock
                .Setup(x => x.GetJwt(It.IsAny<User>()))
                .Returns("blah blah blah this is my Jwt Token");

            var sut = new AuthService(authRepositoryMock.Object, jwtServicesMock.Object);

            // Act: 
            var actual = await sut.UserLoginRequestVerification(username, password);

            // Assert:
            authRepositoryMock.VerifyAll();
            jwtServicesMock.VerifyAll();
        }

        [Fact]
        public async Task UserLoginRequestVerification_Returns_ActionResult_When_VerifyUserRequestIsNotValid()
        {
            // Arrange:
            const string username = "";
            const string password = "Password123$";
            
            var userDto = new UserDto();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock
                .Setup(x => x.Login(It.IsAny<UserDto>()))
                .ReturnsAsync((User)null);

            var sut = new AuthService(authRepositoryMock.Object, _jwtServicesStub);

            // Act: 
            var actual = await sut.UserLoginRequestVerification(username, password);

            // Assert:
            authRepositoryMock.VerifyAll();

            Assert.IsType<BadRequestObjectResult>(actual);
        }
    }
}
