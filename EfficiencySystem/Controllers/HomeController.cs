using EfficiencySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EfficiencySystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpError(int statusCode)
        {
            ViewBag.StatusCode = statusCode;

            return View();
        }
    }
}
