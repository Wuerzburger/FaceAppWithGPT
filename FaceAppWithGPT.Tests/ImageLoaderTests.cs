using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class ImageLoaderTests
    {
        private readonly string _testDirectoryPath = Path.Combine(Path.GetTempPath(), "ImageLoaderTests");

        [TestInitialize]
        public void Setup()
        {
            // Setup a temporary directory with test images
            Directory.CreateDirectory(_testDirectoryPath);
            File.WriteAllText(Path.Combine(_testDirectoryPath, "image1.jpg"), "Image 1 content");
            File.WriteAllText(Path.Combine(_testDirectoryPath, "image2.png"), "Image 2 content");
            File.WriteAllText(Path.Combine(_testDirectoryPath, "document.txt"), "Not an image content");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup the test directory after each test
            Directory.Delete(_testDirectoryPath, true);
        }

        [TestMethod]
        public async Task LoadImagesAsync_ReturnsCorrectNumberOfImages()
        {
            // Given
            var imageLoader = new ImageLoader();

            // When
            var images = await imageLoader.LoadImagesAsync(_testDirectoryPath);

            // Then
            Assert.AreEqual(2, images.Count(), "Expected to find 2 images in the test directory.");
        }

        [TestMethod]
        public async Task LoadImagesAsync_ThrowsWhenDirectoryDoesNotExist()
        {
            // Given
            var imageLoader = new ImageLoader();
            var nonExistentDirectory = Path.Combine(_testDirectoryPath, "nonexistent");

            // When & Then
            await Assert.ThrowsExceptionAsync<DirectoryNotFoundException>(() => imageLoader.LoadImagesAsync(nonExistentDirectory));
        }
    }
}
