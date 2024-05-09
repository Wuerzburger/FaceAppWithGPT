using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public interface IImageProcessingService
    {
        Task ResizeImagesAsync(string sourceDirectory, string targetDirectory, int width, int height);
        Task AlignImagesAsync(string sourceDirectory, string targetDirectory, string referenceImage);
        Task MorphFacesAsync(string sourceDirectory, string targetDirectory, int duration, int pause);
    }

}
