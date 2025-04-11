using EclipseLink.Event;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EclipseLink.Tests
{
    public class EventControllerTests
    {
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly EventController _controller;

        public EventControllerTests()
        {
            _mockEventStore = new Mock<IEventStore>();
            _controller = new EventController(_mockEventStore.Object);
        }

        [Fact]
        public void Get_ShouldReturnEvent_WhenEventExists()
        {
            // Arrange
            var eventObj = new EclipseLink.Event.Event(1) { Name = "Test Event" };
            _mockEventStore.Setup(store => store.Get(1)).Returns(eventObj);

            // Act
            var result = _controller.Get(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(eventObj, result.Value);
        }

        [Fact]
        public void Get_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            _mockEventStore.Setup(store => store.Get(1)).Returns((EclipseLink.Event.Event?)null);

            // Act
            var result = _controller.Get(1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
