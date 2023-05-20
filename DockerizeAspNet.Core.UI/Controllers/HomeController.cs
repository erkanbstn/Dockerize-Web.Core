using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace DockerizeAspNet.Core.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileProvider _fileProvider;
        public HomeController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IActionResult ImageShow()
        {
            var images = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().Select(x => x.Name);
            return View(images);
        }
        [HttpPost]
        public IActionResult ImageShow(string name)
        {
            var file = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().Select(x => x.Name == name);
            System.IO.File.Delete("wwwroot/Images/" + name);
            return RedirectToAction("ImageShow");
        }
        public IActionResult ImageSave()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImageSave(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return View();
        }
    }
}