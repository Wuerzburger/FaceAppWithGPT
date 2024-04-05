using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class ImageLoader
    {
        public async Task<IEnumerable<string>> LoadImagesAsync(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The specified directory was not found: {directoryPath}");
            }

            var imageFiles = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));

            // Asynchronously load image files into memory (if necessary)
            // For this example, we're returning the file paths. Adjust based on further requirements.
            return await Task.FromResult(imageFiles);
        }
    }
}
