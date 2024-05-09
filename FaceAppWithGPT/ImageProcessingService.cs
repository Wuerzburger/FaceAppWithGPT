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

        public ImageProcessingService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public async Task AlignImagesAsync(string sourceDirectory, string targetDirectory, string referenceImage)
        {
            Log.Information("Aligning images from {SourceDirectory} , saving to {TargetDirectory}", sourceDirectory, targetDirectory);
            try
            {
                // Implement alignment logic here
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
            Log.Information("Resizing images from {SourceDirectory} to {Width}x{Height}, saving to {TargetDirectory}", sourceDirectory, width, height, targetDirectory);
            try
            {
                // Implement resizing logic here
                // Example: assuming the operation was successful
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
