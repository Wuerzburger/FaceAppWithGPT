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
        private Mock<IFileSystem> fileSystemMock;
        private ImageResizeHandler handler;
        private CliOptions options;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize mock file system and options
            fileSystemMock = new Mock<IFileSystem>();
            handler = new ImageResizeHandler(fileSystemMock.Object);
            options = new CliOptions
            {
                SourceDirectory = @"C:\FakeSource",
                OutputDirectory = @"C:\FakeOutput",
                Resize = "800x600"
            };

            // Setup mock responses
            fileSystemMock.Setup(fs => fs.Directory.Exists(options.SourceDirectory)).Returns(true);
            fileSystemMock.Setup(fs => fs.Directory.GetFiles(options.SourceDirectory))
                           .Returns(new string[] { @"C:\FakeSource\image1.jpg", @"C:\FakeSource\image2.jpg" });
        }

        [TestMethod]
        public void ResizesImages_WhenCalledWithValidOptions()
        {
            // Act
            handler.HandleCommand(options);

            // Assert
            // Verify that the directory existence check was called
            fileSystemMock.Verify(fs => fs.Directory.Exists(options.SourceDirectory), Times.Once);

            // Verify that GetFiles was called to retrieve images from the source directory
            fileSystemMock.Verify(fs => fs.Directory.GetFiles(options.SourceDirectory), Times.Once);

            // As we can't directly test image resizing in a unit test, verify that an output operation was attempted
            // This can indicate the resize operation was completed and an attempt to save the result was made
            fileSystemMock.Verify(fs => fs.File.WriteAllBytes(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Exactly(2));
        }
    }
}
