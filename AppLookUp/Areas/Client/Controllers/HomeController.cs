using AppLookUp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppLookUp.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Go to Index Home");
            return View();
        }

    }
}