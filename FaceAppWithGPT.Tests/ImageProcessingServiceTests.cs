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
    public class ImageProcessingServiceTests
    {
        [TestMethod]
        public async Task AlignImagesAsync_ShouldAlignImagesCorrectly()
        {
            // Given
            var fileSystemMock = new Mock<IFileSystem>();
            var alignerMock = new Mock<ImageAligner>(fileSystemMock.Object);
            var service = new ImageProcessingService(fileSystemMock.Object, null, alignerMock.Object);
            string sourceDirectory = "source";
            string targetDirectory = "target";
            string referenceImage = "ref.jpg";

            // When
            await service.AlignImagesAsync(sourceDirectory, targetDirectory, referenceImage);

            // Then
            alignerMock.Verify(a => a.AlignImage(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());
        }
    }
}
