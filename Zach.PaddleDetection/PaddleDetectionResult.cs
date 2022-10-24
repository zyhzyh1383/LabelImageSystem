

using Sdcb.PaddleDetection;
using System.Collections.Generic;
using System.Drawing;

namespace Zach.PaddleDetection
{
    public class PaddleDetectionResult
    {
        public Image SourceImage { get; set; }
        public Image DetectionImage { get; set; }
        public List<DetectionResult> DetectionResult { get; set; }
    }
}