using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;

namespace FaceDetection.Models
{
    public class FaceViewModel
    {
        public string FileName { get; set; }
        public byte[] Image { get; set; }
        public IList<DetectedFace> Faces { get; set; }
    }
}
