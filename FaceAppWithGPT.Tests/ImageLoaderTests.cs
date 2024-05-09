using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class ImageLoaderTests
    {
        private MockFileSystem _mockFileSystem;
        private ImageLoader _imageLoader;

        [TestInitialize]
        public void Setup()
        {
            _mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\images\image1.jpg", new MockFileData("dummy content") },
                { @"C:\images\image2.png", new MockFileData("dummy content") }
            });
            _imageLoader = new ImageLoader(_mockFileSystem);
        }

        [TestMethod]
        public async Task Given_ExistingDirectory_When_LoadImagesAsync_Then_ReturnImagePaths()
        {
            // Given
            var directoryPath = @"C:\images";

            // When
            var result = await _imageLoader.LoadImagesAsync(directoryPath);

            // Then
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task Given_NonExistingDirectory_When_LoadImagesAsync_Then_ThrowDirectoryNotFoundException()
        {
            // Given
            var directoryPath = @"C:\nonexistent";

            // When & Then
            await Assert.ThrowsExceptionAsync<DirectoryNotFoundException>(() => _imageLoader.LoadImagesAsync(directoryPath));
        }
    }
}
