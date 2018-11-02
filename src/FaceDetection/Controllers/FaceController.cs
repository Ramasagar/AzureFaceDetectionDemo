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
    /// <summary>
    /// Controller for Face Detection
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class FaceController : Controller
    {
        private readonly IOptions<AzureCognativeServicesConfig> config;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaceController"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public FaceController(IOptions<AzureCognativeServicesConfig> config)
        {
            this.config = config;
        }

        /// <summary>
        /// Default View
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new FaceViewModel());
        }

        /// <summary>
        /// HTTP Post Action for when an image file is uploaded
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        [HttpPost("Index")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                    stream.Flush();
                    stream.Position = 0;

                    var faceClient = new FaceClient(new ApiKeyServiceClientCredentials(config.Value.Face.ApiKey))
                    {
                        Endpoint = config.Value.Face.BaseUri
                    };

                    var result = await faceClient.Face.DetectWithStreamAsync(stream, true, true, new[] {
                        FaceAttributeType.Age,
                        FaceAttributeType.Emotion,
                        FaceAttributeType.FacialHair,
                        FaceAttributeType.Gender,
                        FaceAttributeType.Glasses,
                        FaceAttributeType.Hair,
                        FaceAttributeType.Smile }).ConfigureAwait(false);

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