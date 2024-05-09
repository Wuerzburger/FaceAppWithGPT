using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class ImageAlignmentHandlerTests
    {
        [TestMethod]
        public async Task Given_ValidInput_When_HandleCommandAsync_Then_AlignImages()
        {
            // Given
            var mockProcessingService = new Mock<IImageProcessingService>();
            var handler = new ImageAlignmentHandler(mockProcessingService.Object);
            var options = new CliOptions { SourceDirectory = "source", OutputDirectory = "output", ReferenceImage = "ref.jpg" };

            // When
            await handler.HandleCommandAsync(options);

            // Then
            mockProcessingService.Verify(s => s.AlignImagesAsync("source", "output", "ref.jpg"), Times.Once);
        }
    }
}
