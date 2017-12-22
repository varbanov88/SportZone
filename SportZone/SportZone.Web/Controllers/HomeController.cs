using Microsoft.AspNetCore.Mvc;
using SportZone.Data.Models;
using System.Diagnostics;

namespace SportZone.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "NewsZone", new { area = "News" });
            //return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}