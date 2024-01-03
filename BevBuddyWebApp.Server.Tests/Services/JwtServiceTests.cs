using BevBuddyWebApp.Server.Services;
using BevBuddyWebApp.Shared.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BevBuddyWebApp.Server.Tests.Services
{
    public class JwtServiceTests
    {
        [Fact]
        public void GetJwt_Returns_String_When_CalledByAuthService()
        {
            //Arrange:
            var user = new User { Username = "testUser" };

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x.GetSection("JwtSettings:Issuer").Value).Returns("testIssuer");
            configMock.Setup(x => x.GetSection("JwtSettings:Audience").Value).Returns("testAudience");
            configMock.Setup(x => x.GetSection("JwtSettings:Key").Value).Returns("FooBarNotSuperDuperSecretTestKeyBarFoo");

            var sut = new JwtService(configMock.Object);

            //Act:
            var actual = sut.GetJwt(user);

            //Assert:
            Assert.NotNull(actual);

            var handler = new JwtSecurityTokenHandler();
            var mockToken = handler.ReadToken(actual) as JwtSecurityToken;

            Assert.NotNull(mockToken);
            Assert.Equal("testIssuer", mockToken.Issuer);
            Assert.Equal("testAudience", mockToken.Audiences?.FirstOrDefault());
            Assert.Contains(mockToken.Claims, claim =>
                claim.Type == ClaimTypes.Name && claim.Value == "testUser");
        }
    }
}
