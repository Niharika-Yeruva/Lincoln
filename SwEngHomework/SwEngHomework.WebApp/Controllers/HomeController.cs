using Microsoft.AspNetCore.Mvc;
using SwEngHomework.WebApp.Models;
using System;
using System.Diagnostics;

namespace SwEngHomework.WebApp.Controllers
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
            DateTime currentDateTime = DateTime.Now;
            ViewBag.DateTime = ConvertToUniversalIso8601(currentDateTime);
            ViewBag.DisplayLink = false;
            if (DateTime.TryParse(ViewBag.DateTime, out DateTime dateTime))
            {
                int minute = dateTime.Minute;

                ViewBag.DisplayLink = minute % 2 == 0;
            }
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

        private string ConvertToUniversalIso8601(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("u").Replace(" ", "T");
        }
    }
}