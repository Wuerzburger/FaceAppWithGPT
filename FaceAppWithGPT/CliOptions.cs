using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

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

        [Option('d', "duration", Required = false, HelpText = "Specify the overall duration of how long it takes to morph from one picture to another.")]
        public int Duration { get; set; } = 1; // Default value, adjust as needed

        [Option('p', "pause", Required = false, HelpText = "Specify the duration on how long the second picture after the morphing is displayed until a new morphing sequence is started.")]
        public int Pause { get; set; } = 1; // Default value, adjust as needed

        // Additional CLI options can be defined here.

        public static void DisplayHelp<T>(ParserResult<T> result)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                // Customize HelpText here if needed before returning
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);

            Console.WriteLine(helpText);
        }
    }
}
