using Microsoft.AspNetCore.Mvc;
using PDFGenerationApplication.Models;
using System.Diagnostics;
using PDFGenerationApplication.Repository;

namespace PDFGenerationApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Represents the view to download the document 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Generate(UserModel model)
        {
            byte[] data = await HTMLToByte.GenerateString(model);
            return File(data, "application/pdf", "output.pdf");                                                                                  
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}