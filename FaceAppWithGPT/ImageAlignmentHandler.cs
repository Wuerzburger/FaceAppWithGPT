﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public class ImageAlignmentHandler : ICommandHandler
    {
        private readonly IImageProcessingService _imageProcessingService;

        public ImageAlignmentHandler(IImageProcessingService imageProcessingService)
        {
            _imageProcessingService = imageProcessingService;
        }

        public void HandleCommand(CliOptions options)
        {
            _imageProcessingService.AlignImages(options.SourceDirectory, options.OutputDirectory, options.ReferenceImage);
        }
    }
}
