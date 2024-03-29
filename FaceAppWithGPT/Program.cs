// See https://aka.ms/new-console-template for more information
using FaceAppWithGPT;

try
{
    var options = CliParser.ParseArguments(args);
    if (options != null)
    {
        // Proceed with the application flow using parsed options
        Console.WriteLine("Command-line options parsed successfully.");
        // Implementation for using options in the application's workflow
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    // In a real-world scenario, display help text here
}
