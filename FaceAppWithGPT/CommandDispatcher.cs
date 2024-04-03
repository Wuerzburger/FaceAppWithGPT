using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class CommandDispatcher
    {
        private readonly List<ICommandHandler> _handlers;

        public CommandDispatcher(IImageProcessingService imageProcessingService, IVideoGenerationService videoGenerationService)
        {
            _handlers = new List<ICommandHandler>
        {
            new ImageResizeHandler(imageProcessingService),
            new ImageAlignmentHandler(imageProcessingService),
            new FaceMorphingHandler(imageProcessingService),
            new VideoGenerationHandler(videoGenerationService)
        };
        }

        public void Dispatch(CliOptions options)
        {
            foreach (var handler in _handlers)
            {
                handler.HandleCommand(options);
            }
        }
    }
}
