using System;
using System.Collections.Generic;
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
            handlers = new List<ICommandHandler>
            {
                new ImageResizeHandler(),
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
