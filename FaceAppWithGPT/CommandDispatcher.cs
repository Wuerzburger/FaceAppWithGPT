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
        private readonly List<ICommandHandler> handlers;

        public CommandDispatcher()
        {
            var fileSystem = new FileSystem(); // Concrete implementation for actual file operations
            handlers = new List<ICommandHandler>
            {
                new ImageResizeHandler(fileSystem),
                new ImageAlignmentHandler(),
                new FaceMorphingHandler(),
                new VideoGenerationHandler()
            };
        }

        public void Dispatch(CliOptions options)
        {
            foreach (var handler in handlers)
            {
                handler.HandleCommand(options);
            }
        }
    }
}
