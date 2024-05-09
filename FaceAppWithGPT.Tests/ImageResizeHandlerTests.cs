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
        public async Task HandleCommand_ResizesImages()
        {
            // Arrange
            var mockProcessingService = new Mock<IImageProcessingService>();
            var handler = new ImageResizeHandler(mockProcessingService.Object);
            var options = new CliOptions
            {
                SourceDirectory = "source",
                OutputDirectory = "destination",
                Resize = "800x600"
            };

            // Act
            await handler.HandleCommandAsync(options); // Updated to async

            // Assert
            mockProcessingService.Verify(s => s.ResizeImagesAsync("source", "destination", 800, 600), Times.Once);
        }
    }
}
