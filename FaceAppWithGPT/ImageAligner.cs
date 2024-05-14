using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace FaceAppWithGPT
{
    public class ImageAligner
    {
        private readonly IFileSystem _fileSystem;

        public ImageAligner(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Mat AlignImage(string referenceImagePath, string imagePath)
        {
            // Correctly read images from file paths
            var refImg = CvInvoke.Imread(_fileSystem.Path.Combine(referenceImagePath), ImreadModes.Color);
            var image = CvInvoke.Imread(_fileSystem.Path.Combine(imagePath), ImreadModes.Color);

            using var featureDetector = new SIFT();
            var refKeyPoints = new VectorOfKeyPoint();
            var imageKeyPoints = new VectorOfKeyPoint();
            var refDescriptors = new Mat();
            var imageDescriptors = new Mat();

            featureDetector.DetectAndCompute(refImg, null, refKeyPoints, refDescriptors, false);
            featureDetector.DetectAndCompute(image, null, imageKeyPoints, imageDescriptors, false);

            using var matcher = new BFMatcher(DistanceType.L2);
            var matches = new VectorOfVectorOfDMatch();
            matcher.KnnMatch(refDescriptors, imageDescriptors, matches, 2);

            var goodMatches = new VectorOfDMatch();
            for (int i = 0; i < matches.Size; i++)
            {
                var match = matches[i];
                if (match.Size >= 2 && match[0].Distance < 0.75 * match[1].Distance)
                {
                    goodMatches.Push(new[] { match[0] });
                }
            }

            var points1 = ExtractPoints(refKeyPoints, goodMatches, queryIdx: false);
            var points2 = ExtractPoints(imageKeyPoints, goodMatches, queryIdx: true);
            var homography = CvInvoke.FindHomography(points1, points2, RobustEstimationAlgorithm.Ransac, 5.0);

            var result = new Mat();
            CvInvoke.WarpPerspective(image, result, homography, refImg.Size, Inter.Linear, Warp.Default, BorderType.Reflect);
            return result;
        }

        private PointF[] ExtractPoints(VectorOfKeyPoint keyPoints, VectorOfDMatch matches, bool queryIdx)
        {
            PointF[] points = new PointF[matches.Size];
            for (int i = 0; i < matches.Size; i++)
            {
                int index = queryIdx ? matches[i].QueryIdx : matches[i].TrainIdx;
                points[i] = keyPoints[index].Point;
            }
            return points;
        }
    }
}
