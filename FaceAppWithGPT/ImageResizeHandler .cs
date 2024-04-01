using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class ImageResizeHandler : ICommandHandler
    {
        public void HandleCommand(CliOptions options)
        {
            // Logic to resize images based on options.Resize
            Console.WriteLine("Resizing images...");
        }
    }
}
