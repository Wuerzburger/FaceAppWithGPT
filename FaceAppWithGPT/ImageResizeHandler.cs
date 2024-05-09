using Serilog;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class ImageResizeHandler : ICommandHandler
    {
        private readonly IFileSystem _fileSystem;

        private readonly IImageProcessingService _imageProcessingService;

        public ImageResizeHandler(IImageProcessingService imageProcessingService)
        {
            _imageProcessingService = imageProcessingService;
        }

        public async Task HandleCommandAsync(CliOptions options)
        {
            Log.Information("Starting image resize process for {SourceDirectory}", options.SourceDirectory);
            try
            {
                var sizeParts = options.Resize.Split('x');
                int width = int.Parse(sizeParts[0]);
                int height = int.Parse(sizeParts[1]);

                await _imageProcessingService.ResizeImagesAsync(options.SourceDirectory, options.OutputDirectory, width, height);
                Log.Information("Image resize process completed successfully for {SourceDirectory}", options.SourceDirectory);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during image resize process for {SourceDirectory}", options.SourceDirectory);
                throw;
            }
        }
    }
}
