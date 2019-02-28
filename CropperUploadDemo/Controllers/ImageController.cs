using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CropperUploadDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ImageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public IActionResult Upload()
        {
            var request = HttpContext.Request;
            if (!request.Form.ContainsKey("avatar"))
                return Ok("empty");
            var source = request.Form["avatar"].ToString();
            string base64 = source.Substring(source.IndexOf(',') + 1);
            var base64array = Convert.FromBase64String(base64);
            var filePath = Path.Combine($"{_hostingEnvironment.WebRootPath}/img/{Guid.NewGuid()}.png");
            System.IO.File.WriteAllBytes(filePath, base64array);
            return Ok("success");
        }
    }
}