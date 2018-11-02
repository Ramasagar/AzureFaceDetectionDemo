using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Options;

namespace FaceDetection.Controllers
{
    public class FaceController : Controller
    {
        private readonly IOptions<AzureCognativeServicesConfig> config;

        public FaceController(IOptions<AzureCognativeServicesConfig> config)
        {
            this.config = config;
        }
        public IActionResult Index()
        {
            return View(new FaceViewModel());
        }

        [HttpPost("Index")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Flush();
                    stream.Position = 0;

                    var faceClient = new FaceClient(new ApiKeyServiceClientCredentials(config.Value.Face.ApiKey));
                    faceClient.Endpoint = config.Value.Face.BaseUri;

                    var result = await faceClient.Face.DetectWithStreamAsync(stream, true, true, new[] { FaceAttributeType.Age, FaceAttributeType.Emotion, FaceAttributeType.FacialHair, FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.Smile });

                    var model = new FaceViewModel
                    {
                        Faces = result,
                        FileName = file.FileName,
                        Image = System.IO.File.ReadAllBytes(filePath)
                    };

                    return View(model);
                }
            }
            return View(new FaceViewModel());
        }
    }
}