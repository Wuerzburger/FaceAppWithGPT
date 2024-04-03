using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public interface IImageProcessingService
    {
        void ResizeImages(string sourceDirectory, string targetDirectory, string size);
        void AlignImages(string sourceDirectory, string targetDirectory, string referenceImage);
        void MorphFaces(string sourceDirectory, string targetDirectory, int duration, int pause);
    }
}
