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

        public void HandleCommand(CliOptions options)
        {
            _imageProcessingService.ResizeImages(options.SourceDirectory, options.OutputDirectory, options.Resize);
        }
    }
}
