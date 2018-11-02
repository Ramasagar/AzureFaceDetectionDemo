using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceDetection
{
    public class AzureCognativeServicesConfig
    {
        public FaceConfig Face { get; set; }
    }

    public class FaceConfig
    {
        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
    }
}
