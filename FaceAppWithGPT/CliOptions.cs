using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace FaceAppWithGPT
{
    public class CliOptions
    {
        [Option('s', "source-dir", Required = true, HelpText = "Specify the source directory containing images.")]
        public string SourceDirectory { get; set; }

        [Option('r', "resize", Required = false, HelpText = "Specify the size to which images should be resized.")]
        public string Resize { get; set; } = "default";

        [Option('a', "align", Required = false, HelpText = "Specify the reference image for alignment.")]
        public string ReferenceImage { get; set; }

        [Option('o', "output-dir", Required = true, HelpText = "Specify the output directory for processed images.")]
        public string OutputDirectory { get; set; }

        [Option('f', "fps", Required = true, HelpText = "Specify the frames per second for the video.")]
        public int FramesPerSecond { get; set; }

        [Option('n', "output-name", Required = true, HelpText = "Specify the name of the output video file.")]
        public string OutputName { get; set; }

        // Additional CLI options can be defined here.
    }
}
