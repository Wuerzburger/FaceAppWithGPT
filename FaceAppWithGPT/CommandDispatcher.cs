using Serilog;
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
            Log.Information("Dispatching command based on CLI options");

            try
            {
                foreach (var handler in _handlers)
                {
                    handler.HandleCommand(options);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during command dispatching");
                throw; // Depending on your error handling strategy, you might not want to rethrow.
            }
        }
    }
}
