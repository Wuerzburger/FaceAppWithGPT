using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class CliParserTests
    {
        [TestMethod]
        public void ParsesValidArguments()
        {
            var args = new string[] { "-s", "sourceDir", "-o", "outputDir", "-f", "30", "-n", "video.mp4" };
            var options = CliParser.ParseArguments(args);
            Assert.IsNotNull(options, "Options should not be null for valid arguments.");
            // Additional assertions to validate option values go here
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionForInvalidArguments()
        {
            var args = new string[] { "-invalid", "option" };
            var options = CliParser.ParseArguments(args);
            // Expecting an ArgumentException to be thrown by CliParser
        }
    }
}
