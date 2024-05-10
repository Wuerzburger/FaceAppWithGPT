using Emgu.CV.Structure;
using Emgu.CV;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly IFileSystem _fileSystem;
        private readonly ImageResizer _imageResizer;

        public ImageProcessingService(IFileSystem fileSystem, ImageResizer imageResizer)
        {
            _fileSystem = fileSystem;
            _imageResizer = imageResizer;
        }

        public async Task AlignImagesAsync(string sourceDirectory, string targetDirectory, string referenceImage)
        {
            Log.Information("Starting alignment of images in {SourceDirectory} using {ReferenceImage}", sourceDirectory, referenceImage);
            try
            {
                var refImgPath = _fileSystem.Path.Combine(sourceDirectory, referenceImage);
                var refImg = new Image<Bgr, byte>(refImgPath);

                foreach (var imagePath in _fileSystem.Directory.GetFiles(sourceDirectory))
                {
                    var image = new Image<Bgr, byte>(imagePath);
                    // Add alignment logic here using OpenCV functions...
                    var alignedImagePath = _fileSystem.Path.Combine(targetDirectory, _fileSystem.Path.GetFileName(imagePath));
                    image.Save(alignedImagePath); // Save aligned image
                }

                Log.Information("Successfully aligned images and saved to {TargetDirectory}", targetDirectory);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to align images from {SourceDirectory}", sourceDirectory);
                throw;
            }
        }

        public Task MorphFacesAsync(string sourceDirectory, string targetDirectory, int duration, int pause)
        {
            throw new NotImplementedException();
        }
        public async Task ResizeImagesAsync(string sourceDirectory, string targetDirectory, int width, int height)
        {
            Log.Information("Starting resizing of images in {SourceDirectory} to {Width}x{Height}, saving to {TargetDirectory}", sourceDirectory, width, height, targetDirectory);
            try
            {
                var files = _fileSystem.Directory.GetFiles(sourceDirectory, "*.*", SearchOption.TopDirectoryOnly)
                                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));

                foreach (var file in files)
                {
                    var destinationPath = _fileSystem.Path.Combine(targetDirectory, _fileSystem.Path.GetFileName(file));
                    await _imageResizer.ResizeImageAsync(file, destinationPath, width, height);
                }

                Log.Information("Successfully resized images and saved to {TargetDirectory}", targetDirectory);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to resize images from {SourceDirectory}", sourceDirectory);
                throw;
            }
        }

    }
}
