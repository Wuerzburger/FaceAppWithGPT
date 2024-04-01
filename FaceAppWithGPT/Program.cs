// See https://aka.ms/new-console-template for more information
using FaceAppWithGPT;

try
{
    var options = CliParser.ParseArguments(args);
    if (options != null)
    {
        var dispatcher = new CommandDispatcher();
        dispatcher.Dispatch(options);
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    // In a real-world scenario, display help text here
}
