using interview.test.ensek.Client.Uploader.Services;
using Microsoft.AspNetCore.Mvc;

namespace interview.test.ensek.Client.Uploader.Controllers
{
    public class FileUploaderController : Controller
    {
        public FileUploaderController(IFileUploaderService fileUploaderService)
        {
            _fileUploaderService = fileUploaderService;
        }

        private IFileUploaderService _fileUploaderService { get; init; }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Index")]
        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file, CancellationToken cancellation)
        {
            try
            {
                var result = await _fileUploaderService.UploadFileAsync(file, cancellation);
                return View(result);
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }
    }
}
