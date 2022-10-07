using HaikuLab3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HaikuLab3.Controllers
{
    public class StartController : Controller
    {
        private readonly ILogger<StartController> _logger;

        public StartController(ILogger<StartController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            TopHaikuMethods thm = new TopHaikuMethods();
            ViewModelToplist vmtl = new ViewModelToplist
            {
                TopHaikuDetailList = thm.SelectTopHaikuList(out string errormsg),
                TopRatingDetailList = thm.SelectTopRatingList(out string errormsg2)
            };


            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(vmtl);


        
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ShowAllHaiku()
        {
            List<HaikuListDetail> Haikulist = new List<HaikuListDetail>();
            HaikuListMethods hm = new HaikuListMethods();
            string error = "";
            Haikulist = hm.SelectHaikuList(out error);
            ViewBag.error = error;
            return View(Haikulist);
        }

        [HttpGet]
        public IActionResult FilterHaikuList()
        {
            HaikuListMethods hlm = new HaikuListMethods();
            GenreMethods gm = new GenreMethods();
            ViewModelHaikuList vmhl = new ViewModelHaikuList
            {
                HaikuListDetailList = hlm.SelectHaikuList(out string errormsg),
                GenreDetailList = gm.SelectGenreList(out string errormsg2),
            };

            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(vmhl);
                        
            }

        [HttpPost]
        public IActionResult FilterHaikuList(string Ge_Name)
        {
            HaikuListMethods hlm = new HaikuListMethods();
            GenreMethods gm = new GenreMethods();
            ViewModelHaikuList vmhl = new ViewModelHaikuList
            {
                HaikuListDetailList = hlm.SelectHaikuList(out string errormsg, Ge_Name),
                GenreDetailList = gm.SelectGenreList(out string errormsg2)
            };

            
            ViewData["TestViewData"] = Ge_Name;
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(vmhl);
            
        }

        [HttpGet]
        public IActionResult SearchHaikuList()
        {
            HaikuListMethods hlm = new HaikuListMethods();
            ViewModelHaikuList vmhl = new ViewModelHaikuList
            {
                HaikuListDetailList = hlm.SelectHaikuList(out string errormsg)
            };

            ViewBag.error = "1: " + errormsg;
            return View(vmhl);

        }

        [HttpPost]
        public IActionResult SearchHaikuList(string search)
        {
            HaikuListMethods hlm = new HaikuListMethods();
            ViewModelHaikuList vmhl = new ViewModelHaikuList
            {
                HaikuListDetailList = hlm.SearchHaikuList(out string errormsg, search)
            };

            ViewData["SearchHaiku"] = search;
            ViewBag.error = "1: " + errormsg;
            return View(vmhl);

        }

        [HttpGet]
        public IActionResult Details(string id)
        {          
            HaikuMethods hm = new HaikuMethods();
            ViewModelHaikuRating vmhr = new ViewModelHaikuRating
            {
                HaikuDetailList = hm.SelectHaiku(out string errormsg, id),
                RatingDetailList = hm.SelectRating(out string errormsg2, id)
            };

           
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(vmhr);

        }
        [HttpGet]

        public IActionResult InsertRatingForm(string id)
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Home");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            
            ViewBag.jsonstring = jsonstring;
            TempData["Test"] = alias;
            TempData["Titel"] = id;

            return View();


        }

        [HttpPost]

        public IActionResult InsertRatingForm(string id, string alias, int rating)
        {
            
            RatingDetail ratingDetail = new RatingDetail();
            RatingMethods rm = new RatingMethods();

            int i = 0;
            string error = "";

            i = rm.InsertRating(id, alias, rating, out error);

            ViewBag.error = error;
            ViewBag.antal = i;

            if (error == "")
            {
                return RedirectToAction("InsertRatingSuccess");
            }
            else
            {
                return View("InsertRatingFail");
            }

        }

        public IActionResult InsertRatingFail()
        {
            return View();
        }

        public IActionResult InsertRatingSuccess()
        {
            return View();
        }



    }
}