using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{

    public class ImageLoader
    {
        private readonly IFileSystem _fileSystem;

        public ImageLoader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<IEnumerable<string>> LoadImagesAsync(string directoryPath)
        {
            if (!_fileSystem.Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The specified directory was not found: {directoryPath}");
            }

            var imageFiles = _fileSystem.Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));

            return await Task.FromResult(imageFiles);
        }
    }
}
