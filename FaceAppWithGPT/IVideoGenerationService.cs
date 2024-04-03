using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public interface IVideoGenerationService
    {
        void GenerateVideo(string sourceDirectory, string outputFilePath, int fps);
    }
}
