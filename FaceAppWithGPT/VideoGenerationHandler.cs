using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class VideoGenerationHandler : ICommandHandler
    {
        private readonly IVideoGenerationService _videoGenerationService;

        public VideoGenerationHandler(IVideoGenerationService videoGenerationService)
        {
            _videoGenerationService = videoGenerationService;
        }

        public void HandleCommand(CliOptions options)
        {
            _videoGenerationService.GenerateVideo(options.SourceDirectory, options.OutputName, options.FramesPerSecond);
        }
    }
}
