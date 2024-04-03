using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class ImageResizeHandlerTests
    {
        [TestMethod]
        public void ResizeImages_CallsProcessingService_WithCorrectParameters()
        {
            // Arrange
            var mockImageProcessingService = new Mock<IImageProcessingService>();
            var handler = new ImageResizeHandler(mockImageProcessingService.Object);
            var options = new CliOptions
            {
                SourceDirectory = "source",
                OutputDirectory = "output",
                Resize = "800x600"
            };

            // Act
            handler.HandleCommand(options);

            // Assert
            mockImageProcessingService.Verify(service =>
                service.ResizeImages("source", "output", "800x600"), Times.Once);
        }
    }
}
