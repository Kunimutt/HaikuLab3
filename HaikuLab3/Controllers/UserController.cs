using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;
using Newtonsoft.Json;

namespace HaikuLab3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertUser()
        {
            return View();

        }

        public IActionResult InsertUserFail()
        {
            return View();

        }

        [HttpGet]
        public IActionResult InsertUserForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertUserForm(UserDetail ud)
        {
            
            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";

            i = um.InsertUser(ud, out error);

            ViewBag.error = error;
            ViewBag.antal = i;

            if (error == "")
            {
                string u = JsonConvert.SerializeObject(ud);
                HttpContext.Session.SetString("testSession", u);
                return View("InsertUser");
            } else
            {
                return View("InsertUserFail");
            }
            


        }

        [HttpGet]
        public IActionResult TestSessionVar(UserDetail ud)
        {
            UserDetail uu = new UserDetail();
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return View("Index");
            }
            uu = JsonConvert.DeserializeObject<UserDetail>(jsonstring);
            ViewBag.jsonstring = jsonstring;

            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";

            i = um.UpdateUser(uu, out error);

            ViewBag.error = error;
            ViewBag.antal = i;


            return View(uu);
            
        }

        
    }
}
