using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT.Tests
{
    [TestClass]
    public class ImageResizeHandlerTests
    {
        [TestMethod]
        public void ImageResizeHandler_CallsResizeMethod()
        {
            var handler = new ImageResizeHandler();
            var options = new CliOptions { Resize = "800x600" };

            // Simulate handling the command (you may need to mock dependencies)
            handler.HandleCommand(options);

            // Assertions to verify the resize operation was called as expected
            Assert.IsTrue(/* condition to verify the resize operation */);
        }
    }
}
