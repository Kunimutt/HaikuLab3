using HaikuLab3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HaikuLab3.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ShowAllHaiku()
        {
            List<HaikuList> Haikulist = new List<HaikuList>();
            HaikuListMethods hm = new HaikuListMethods();
            string error = "";
            Haikulist = hm.SelectHaikuList(out error);
            ViewBag.error = error;
            return View(Haikulist);
        }

        [HttpGet]
        public IActionResult FilterHaiku()
        { return View(); 
        }

        [HttpPost]
        public IActionResult FilterHaiku(HaikuList haikuList)
        {
            List<HaikuList> Haikulist = new List<HaikuList>();
            HaikuListMethods hm = new HaikuListMethods();
            string error = "";
            Haikulist = hm.FilterHaikuList(haikuList, out error);
            ViewBag.error = error;
            return View(Haikulist);
        }
    }
}