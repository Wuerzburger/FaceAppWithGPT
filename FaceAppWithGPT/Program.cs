// See https://aka.ms/new-console-template for more information
using FaceAppWithGPT;
using Serilog;

try
{
    var imageProcessingService = new ImageProcessingService();
    var videoGenerationService = new VideoGenerationService();
    var dispatcher = new CommandDispatcher(imageProcessingService, videoGenerationService);

    Log.Information("Application starting up");
    var options = CliParser.ParseArguments(args);
    if (options != null)
    {
        Log.Information("CLI parsing succeeded");
        dispatcher.Dispatch(options);
    }
    else
    {
        Log.Warning("CLI parsing failed or resulted in no operation");
    }

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensures all logs are flushed and logged before application exit
}
