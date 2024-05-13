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
            var refImg = new Image<Bgr, byte>(_fileSystem.File.OpenRead(referenceImagePath)).Mat;
            var image = new Image<Bgr, byte>(_fileSystem.File.OpenRead(imagePath)).Mat;

            using var featureDetector = new SIFT();
            var refKeyPoints = new VectorOfKeyPoint();
            var refDescriptors = new Mat();
            featureDetector.DetectAndCompute(refImg, null, refKeyPoints, refDescriptors, false);

            var keyPoints = new VectorOfKeyPoint();
            var descriptors = new Mat();
            featureDetector.DetectAndCompute(image, null, keyPoints, descriptors, false);

            using var matcher = new BFMatcher(DistanceType.L2);
            var matches = new VectorOfVectorOfDMatch();
            matcher.KnnMatch(refDescriptors, descriptors, matches, 2);

            // Use Lowe's ratio test
            var goodMatches = new VectorOfDMatch();
            for (int i = 0; i < matches.Size; i++)
            {
                if (matches[i].Size >= 2 && matches[i][0].Distance < 0.75 * matches[i][1].Distance)
                {
                    goodMatches.Push(new[] { matches[i][0] });
                }
            }

            var homography = CvInvoke.FindHomography(ExtractPoints(keyPoints, goodMatches, true), ExtractPoints(refKeyPoints, goodMatches, false), HomographyMethod.Ransac, 5);
            var aligned = new Mat();
            CvInvoke.WarpPerspective(image, aligned, homography, refImg.Size);
            return aligned;
        }

        private PointF[] ExtractPoints(VectorOfKeyPoint keyPoints, VectorOfDMatch matches, bool query)
        {
            PointF[] pts = new PointF[matches.Size];
            for (int i = 0; i < matches.Size; i++)
            {
                int idx = query ? matches[i].QueryIdx : matches[i].TrainIdx;
                pts[i] = keyPoints[idx].Point;
            }
            return pts;
        }
    }
}
