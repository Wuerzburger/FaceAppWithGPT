using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public static class CliParser
    {
        public static CliOptions ParseArguments(string[] args)
        {
            CliOptions parsedOptions = null;
            Parser.Default.ParseArguments<CliOptions>(args)
                .WithParsed(options => parsedOptions = options)
                .WithNotParsed(errors => HandleParseError(errors));
            return parsedOptions;
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            // Handle errors or invalid arguments
            // For simplicity, we're just throwing an exception here.
            // In a real-world application, you might want to show usage help instead.
            throw new ArgumentException("Invalid command line arguments.");
        }
    }
}
