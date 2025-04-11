using EclipseLink.CommunityInteraction;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EclipseLink.Tests
{
    public class CommunityInteractionControllerTests
    {
        [Fact]
        public void Controller_ShouldBeInitialized()
        {
            // Arrange & Act
            var controller = new CommunityInteractionController();

            // Assert
            Assert.NotNull(controller);
        }
    }
}
