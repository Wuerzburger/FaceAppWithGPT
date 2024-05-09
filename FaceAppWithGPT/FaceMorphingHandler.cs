using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class FaceMorphingHandler : ICommandHandler
    {
        private readonly IImageProcessingService _imageProcessingService;

        public FaceMorphingHandler(IImageProcessingService imageProcessingService)
        {
            _imageProcessingService = imageProcessingService;
        }

        public async Task HandleCommandAsync(CliOptions options)
        {
            await _imageProcessingService.MorphFacesAsync(options.SourceDirectory, options.OutputDirectory, options.Duration, options.Pause);
        }
    }
}
