// See https://aka.ms/new-console-template for more information
using FaceAppWithGPT;

try
{
    var imageProcessingService = new ImageProcessingService();
    var videoGenerationService = new VideoGenerationService();
    var dispatcher = new CommandDispatcher(imageProcessingService, videoGenerationService);

    var options = CliParser.ParseArguments(args);
    if (options != null)
    {
        dispatcher.Dispatch(options);
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    // In a real-world scenario, display help text here
}
