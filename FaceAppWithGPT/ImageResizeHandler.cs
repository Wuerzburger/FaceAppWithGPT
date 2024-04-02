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

        public ImageResizeHandler(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void HandleCommand(CliOptions options)
        {
            // Implement the resize logic using _fileSystem
            Console.WriteLine("Resizing images...");
        }
    }
}
