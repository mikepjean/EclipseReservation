using EclipseLink.UserEventManagement;
using EclipseLink.UserManagement;
using EclipseLink.EventManagement;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EclipseLink.Tests
{
    public class UserEventControllerTests
    {
        private readonly Mock<IUserEventStore> _mockUserEventStore;
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly Mock<IUserStore> _mockUserStore;
        private readonly UserEventController _controller;

        public UserEventControllerTests()
        {
            _mockUserEventStore = new Mock<IUserEventStore>();
            _mockEventStore = new Mock<IEventStore>();
            _mockUserStore = new Mock<IUserStore>();
            _controller = new UserEventController(_mockUserEventStore.Object, _mockEventStore.Object, _mockUserStore.Object);
        }

        [Fact]
        public void RSVP_ShouldReturnOk_WhenRSVPIsSuccessful()
        {
            // Arrange
            var user = new EclipseLink.UserManagement.User { User_Id = 1, Username = "johndoe" };
            var eventObj = new EclipseLink.EventManagement.Event(1) { Name = "Test Event" };

            _mockUserStore.Setup(store => store.GetUserById(1)).Returns(user);
            _mockEventStore.Setup(store => store.Get(1)).Returns(eventObj);
            _mockUserEventStore.Setup(store => store.Get(1)).Returns((EclipseLink.UserEventManagement.UserEvent?)null);

            // Act
            var result = _controller.RSVP(1, 1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RSVP_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserStore.Setup(store => store.GetUserById(1)).Returns((EclipseLink.UserManagement.User?)null);

            // Act
            var result = _controller.RSVP(1, 1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
