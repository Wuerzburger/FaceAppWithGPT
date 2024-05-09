using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class VideoGenerationHandlerTests
    {
        [TestMethod]
        public void GenerateVideo_CallsVideoService_WithCorrectParameters()
        {
            // Arrange
            var mockVideoGenerationService = new Mock<IVideoGenerationService>();
            var handler = new VideoGenerationHandler(mockVideoGenerationService.Object);
            var options = new CliOptions
            {
                SourceDirectory = "source",
                OutputName = "video.mp4",
                FramesPerSecond = 30
            };

            // Act
            handler.HandleCommandAsync(options);

            // Assert
            mockVideoGenerationService.Verify(service =>
                service.GenerateVideo("source", "video.mp4", 30), Times.Once);
        }
    }
}
