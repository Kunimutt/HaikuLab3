using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;

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


            return View("InsertUser");
        }

        
    }
}
