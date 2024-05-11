using Emgu.CV.Structure;
using Emgu.CV;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace FaceAppWithGPT
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly IFileSystem _fileSystem;
        private readonly ImageResizer _imageResizer;

        public ImageProcessingService(IFileSystem fileSystem, ImageResizer imageResizer)
        {
            _fileSystem = fileSystem;
            _imageResizer = imageResizer;
        }

        public async Task AlignImagesAsync(string sourceDirectory, string targetDirectory, string referenceImage)
        {
            Log.Information("Starting alignment of images in {SourceDirectory} using {ReferenceImage}", sourceDirectory, referenceImage);
            try
            {
                string refImagePath = _fileSystem.Path.Combine(sourceDirectory, referenceImage);
                var refImg = new Mat(refImagePath, ImreadModes.Color);
                var refKeyPoints = new VectorOfKeyPoint();
                var refDescriptors = new Mat();

                using (var featureDetector = new SIFT())
                {
                    featureDetector.DetectAndCompute(refImg, null, refKeyPoints, refDescriptors, false);
                }

                foreach (var imagePath in _fileSystem.Directory.GetFiles(sourceDirectory))
                {
                    if (imagePath.EndsWith(".jpg") || imagePath.EndsWith(".jpeg") || imagePath.EndsWith(".png"))
                    {
                        var img = new Mat(imagePath, ImreadModes.Color);
                        var keyPoints = new VectorOfKeyPoint();
                        var descriptors = new Mat();

                        using (var featureDetector = new SIFT())
                        {
                            featureDetector.DetectAndCompute(img, null, keyPoints, descriptors, false);
                        }

                        var matcher = new BFMatcher(DistanceType.L2);
                        var matches = new VectorOfVectorOfDMatch();
                        matcher.KnnMatch(refDescriptors, descriptors, matches, 2);

                        // Filter matches using Lowe's ratio test
                        var goodMatches = new VectorOfDMatch();
                        for (int i = 0; i < matches.Size; i++)
                        {
                            if (matches[i].Size >= 2 && matches[i][0].Distance < 0.75 * matches[i][1].Distance)
                            {
                                goodMatches.Push(new[] { matches[i][0] });
                            }
                        }

                        // Align and save the image
                        var alignedImage = AlignImage(refImg, img, refKeyPoints, keyPoints, goodMatches);
                        alignedImage.Save(_fileSystem.Path.Combine(targetDirectory, _fileSystem.Path.GetFileName(imagePath)));
                    }
                }

                Log.Information("Successfully aligned images and saved to {TargetDirectory}", targetDirectory);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to align images from {SourceDirectory}", sourceDirectory);
                throw;
            }
        }

        private Mat AlignImage(Mat refImg, Mat img, VectorOfKeyPoint refKeyPoints, VectorOfKeyPoint keyPoints, VectorOfDMatch matches)
        {
            // Extract points and compute homography
            var pts1 = new VectorOfPointF();
            var pts2 = new VectorOfPointF();

            for (int i = 0; i < matches.Size; i++)
            {
                pts1.Push(new[] { refKeyPoints[matches[i].TrainIdx].Point });
                pts2.Push(new[] { keyPoints[matches[i].QueryIdx].Point });
            }

            var homography = CvInvoke.FindHomography(pts2, pts1, HomographyMethod.Ransac, 5);
            var aligned = new Mat();
            CvInvoke.WarpPerspective(img, aligned, homography, refImg.Size);
            return aligned;
        }

        public Task MorphFacesAsync(string sourceDirectory, string targetDirectory, int duration, int pause)
        {
            throw new NotImplementedException();
        }
        public async Task ResizeImagesAsync(string sourceDirectory, string targetDirectory, int width, int height)
        {
            Log.Information("Starting resizing of images in {SourceDirectory} to {Width}x{Height}, saving to {TargetDirectory}", sourceDirectory, width, height, targetDirectory);
            try
            {
                var files = _fileSystem.Directory.GetFiles(sourceDirectory, "*.*", SearchOption.TopDirectoryOnly)
                                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));

                foreach (var file in files)
                {
                    var destinationPath = _fileSystem.Path.Combine(targetDirectory, _fileSystem.Path.GetFileName(file));
                    await _imageResizer.ResizeImageAsync(file, destinationPath, width, height);
                }

                Log.Information("Successfully resized images and saved to {TargetDirectory}", targetDirectory);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to resize images from {SourceDirectory}", sourceDirectory);
                throw;
            }
        }

    }
}
