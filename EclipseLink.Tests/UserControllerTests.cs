using EclipseLink.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EclipseLink.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserStore> _mockUserStore;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UserRegistrationController _controller;

        public UserControllerTests()
        {
            _mockUserStore = new Mock<IUserStore>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c["JwtSettings:SecretKey"]).Returns("ThisIsAStrongSecretKey1234567890");
            _mockConfiguration.Setup(c => c["JwtSettings:Issuer"]).Returns("EclipseLink");
            _mockConfiguration.Setup(c => c["JwtSettings:Audience"]).Returns("EclipseLinkAPI");

            _controller = new UserRegistrationController(_mockUserStore.Object, _mockConfiguration.Object);
        }

        [Fact]
        public void Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new EclipseLink.UserManagement.User { User_Id = 1, Username = "johndoe" };
            _mockUserStore.Setup(store => store.GetUserById(1)).Returns(user);

            var loginRequest = new UserRegistrationController.LoginRequest
            {
                UserId = 1,
                Username = "johndoe"
            };

            // Act
            var result = _controller.Login(loginRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Token", result.Value.ToString());
        }

        [Fact]
        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            _mockUserStore.Setup(store => store.GetUserById(1)).Returns((EclipseLink.UserManagement.User?)null);

            var loginRequest = new UserRegistrationController.LoginRequest
            {
                UserId = 1,
                Username = "johndoe"
            };

            // Act
            var result = _controller.Login(loginRequest) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
