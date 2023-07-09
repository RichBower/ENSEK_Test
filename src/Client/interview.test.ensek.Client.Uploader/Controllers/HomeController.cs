using System.Diagnostics;
using interview.test.ensek.Client.Uploader.Models;
using interview.test.ensek.Client.Uploader.Services;
using Microsoft.AspNetCore.Mvc;

namespace interview.test.ensek.Client.Uploader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFileUploaderService _fileUploaderService { get; init; }

        public HomeController(ILogger<HomeController> logger,
            IFileUploaderService fileUploaderService)
        {
            _fileUploaderService = fileUploaderService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
            catch 
            {
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}