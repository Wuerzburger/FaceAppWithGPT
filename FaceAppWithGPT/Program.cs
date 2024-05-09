// See https://aka.ms/new-console-template for more information
using CommandLine;
using FaceAppWithGPT;
using Serilog;
using System.IO.Abstractions;

try
{
    // Instantiate services
    var fileSystem = new FileSystem(); // Create an instance of IFileSystem
    var imageProcessingService = new ImageProcessingService(fileSystem);
    var videoGenerationService = new VideoGenerationService();
    var dispatcher = new CommandDispatcher(imageProcessingService, videoGenerationService);

    // Parse CLI arguments and handle results
    Parser.Default.ParseArguments<CliOptions>(args)
          .WithParsed(options =>
          {
              Log.Information("CLI parsing succeeded");
              dispatcher.Dispatch(options); // Dispatch commands based on parsed options
          })
          .WithNotParsed(errors =>
          {
              // Handle parsing errors, such as missing required arguments or help request
              // CommandLineParser automatically handles help text display for '-h' or '--help'
              if (errors.IsVersion() || errors.IsHelp())
              {
                  // Version or help text requested, handled internally by CommandLineParser
              }
              else
              {
                  Log.Warning("CLI parsing failed or resulted in no operation");
                  // Optionally, provide additional instructions or direct users to detailed documentation
              }
          });
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensure all logs are flushed before application exit
}
