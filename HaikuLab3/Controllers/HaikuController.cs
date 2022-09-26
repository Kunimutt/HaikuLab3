using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;

namespace HaikuLab3.Controllers
{
    public class HaikuController : Controller
    {
        public IActionResult InsertHaiku()
        {
            return View();
        }

        public IActionResult InsertHaikuFail()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult InsertHaikuForm()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult InsertHaikuForm(HaikuDetail hd)
        {
            HaikuMethods hm = new HaikuMethods();
            int i = 0;
            string error = "";

            i = hm.InsertHaiku(hd, out error);

            ViewBag.error = error;
            ViewBag.antal = i;

            if (error == "")
            {
                return View("InsertHaiku");
            }
            else
            {
                return View("InsertHaikuFail");
            }


            //return View();
        }
    }
}
