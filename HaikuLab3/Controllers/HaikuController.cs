using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;
using Newtonsoft.Json;

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
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;
            TempData["Test"] = alias;
            return View();
        }
        
        [HttpPost]
        public IActionResult InsertHaikuForm(HaikuDetail hd)
        {

            if (ModelState.IsValid)
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

            }
                


            return View();
        }
    }
}
