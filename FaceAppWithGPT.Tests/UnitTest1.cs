using CommandLine;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class CliOptionsTests
    {
        [TestMethod]
        public void ParsesValidArgumentsCorrectly()
        {
            string[] args = new string[] { "-s", "sourceDir", "-o", "outputDir", "-f", "30", "-n", "video.mp4" };
            ParserResult<CliOptions> result = Parser.Default.ParseArguments<CliOptions>(args);

            Assert.IsTrue(result.Tag == ParserResultType.Parsed);
            if (result.Tag == ParserResultType.Parsed)
            {
                CliOptions options = ((Parsed<CliOptions>)result).Value;
                Assert.AreEqual("sourceDir", options.SourceDirectory);
                Assert.AreEqual("outputDir", options.OutputDirectory);
                Assert.AreEqual(30, options.FramesPerSecond);
                Assert.AreEqual("video.mp4", options.OutputName);
            }
        }

        [TestMethod]
        public void FailsOnMissingRequiredArguments()
        {
            string[] args = new string[] { "-s", "sourceDir" }; // Missing other required options
            ParserResult<CliOptions> result = Parser.Default.ParseArguments<CliOptions>(args);
            Assert.IsTrue(result.Tag == ParserResultType.NotParsed);
        }

        // Additional tests to cover other scenarios
    }
}