using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;
using Newtonsoft.Json;

namespace HaikuLab3.Controllers
{
    public class HaikuController : Controller
    {
        [HttpGet]
        public IActionResult InsertHaiku()
        {
            string jsonstring2 = HttpContext.Session.GetString("testSession2");
            string rubrik = JsonConvert.DeserializeObject<string>(jsonstring2);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring2 = jsonstring2;

            TempData["rubrik"] = rubrik;
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
                return RedirectToAction("LogInUser", "User");
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
                string rubrik = hd.Ha_Title;

                //hd.Ha_Content = Content1 + " " + Content2 + " " + Content3;

                i = hm.InsertHaiku(hd, out error);

                ViewBag.error = error;
                ViewBag.felmedd = "Ett eventuellt felmeddelande: " + error;
                //ViewData["title"] = rubrik;

                if (error == "")
                {
                    string u2 = JsonConvert.SerializeObject(rubrik);
                    HttpContext.Session.SetString("testSession2", u2);
                    //return View("InsertHaiku");
                    return RedirectToAction("InsertHaiku");
                }
                else 
                {
                    return RedirectToAction("InsertHaikuFail");
                    //return View("InsertHaikuFail");
                }

            }
                


            return RedirectToAction("InsertHaikuForm");
        }
    }
}
