﻿using HaikuLab3.Models;
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
            List<TopHaikuDetail> TopHaikuList = new List<TopHaikuDetail>();
            TopHaikuMethods thm = new TopHaikuMethods();
            string error = "";
            TopHaikuList = thm.SelectTopHaikuList(out error);
            ViewBag.error = error;
            return View(TopHaikuList);
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
            //string filter = Convert.ToString(Ge_Name);
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
            //List<HaikuDetail> Haikuex = new List<HaikuDetail>();
            HaikuDetail Haikuex = new HaikuDetail();
            HaikuMethods hm = new HaikuMethods();
            //string id2 Request.QueryString["id"];
            Haikuex = hm.SelectHaiku(out string errormsg, id);


            ViewBag.error = "1: " + errormsg;
            ViewData["detailtest"] = id;



            return View(Haikuex);


        }

    }
}