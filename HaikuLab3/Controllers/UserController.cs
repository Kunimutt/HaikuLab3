using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;
using Newtonsoft.Json;

namespace HaikuLab3.Controllers
{
    public class UserController : Controller
    {

        public IActionResult InsertUser()
        {
            return View();

        }

        public IActionResult InsertUserFail()
        {
            return View();

        }

       
        public IActionResult AddPic()
        {
            return View();
        }


        [HttpGet]
        public IActionResult InsertUserForm()
        {
            return View();
        }


        //TEST AV SESSIONSVARIABEL SOM SPARAR ALIAS
        [HttpPost]
        public IActionResult InsertUserForm(UserDetail ud)
        {
            if (ModelState.IsValid)
            {
                UserMethods um = new UserMethods();
                int i = 0;
                string error = "";
                string alias = ud.Us_Alias;
                //string photo = ud.Us_Photo;

                i = um.InsertUser(ud, out error);

                ViewBag.error = error;
                ViewBag.antal = i;

                if (error == "")
                {
                    string u = JsonConvert.SerializeObject(alias);
                    HttpContext.Session.SetString("testSession", u);
                    return View("InsertUser");
                }
                else
                {
                    return View("InsertUserFail");
                }


            }
            return View();
        }

        [HttpGet]
        public IActionResult ShowMyPage()
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Home", "Start");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;

            HaikuListMethods hlm = new HaikuListMethods();
            UserMethods um = new UserMethods();
            ViewModelUserHaiku vmuh = new ViewModelUserHaiku
            {
                HaikuListDetailList = hlm.SelectHaikuListForUser(out string errormsg, alias),
                UserDetailList = um.SelectUserList(out string errormsg2, alias)
            };
            TempData["Test"] = alias;
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(vmuh);

        }

        [HttpGet]
        public IActionResult EditEmail()
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Home", "Start");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;
            TempData["Test"] = alias;
            return View();

        }

        [HttpPost]
        public IActionResult EditEmail(string email)
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;


            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";

            i = um.UpdateUserEmail(alias, email, out error);

            ViewBag.error = error;
            ViewBag.antal = i;

            if (error == "")
            {
                return RedirectToAction("ShowMyPage");
            }
            else
            {
                return View("UpdateViewFail");
            }
           

        }

        public IActionResult UpdateViewFail()
        {
            return View();
        }

       

        [HttpGet]
        public IActionResult DeleteUser()
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;


            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";

            i = um.DeleteUser(alias, out error);

            ViewBag.error = error;
            ViewBag.antal = i;

            if (error == "")
            {
                
                HttpContext.Session.Clear();
               
                return RedirectToAction("Home", "Start");
                
            }
            else
            {
                return View("DeleteUserFail");
            }


        }

        public IActionResult DeleteUserFail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogInUser()
        {
            return View();
        }



        [HttpPost]
        public IActionResult LogInUser(string alias, string email)
        {
            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";
            //string alias = ud.Us_Alias;

            i = um.LogInUser(alias, email, out error);

            ViewBag.error = error;
            ViewBag.antal = i;



            if (error == "")
            {
                string u = JsonConvert.SerializeObject(alias);
                HttpContext.Session.SetString("testSession", u);
                return View("LogInUserSuccess");
            }
            else
            {
                return View("LogInUserFail");
            }

        }

        public IActionResult LogInUserSuccess()
        {
            return View();
        }

        public IActionResult LogInUserFail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowProfilePage(string id)
        {
            HaikuListMethods hlm = new HaikuListMethods();
            UserMethods um = new UserMethods();
            ViewModelUserHaiku vmuh = new ViewModelUserHaiku
            {
                HaikuListDetailList = hlm.SelectHaikuListForUser(out string errormsg, id),
                UserDetailList = um.SelectUserList(out string errormsg2, id)
            };

            TempData["user"] = id;

            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(vmuh);

        }

        [HttpGet]
        public IActionResult AddDescription()
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Home", "Start");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;
            TempData["Test"] = alias;
            return View();

        }

        [HttpPost]
        public IActionResult AddDescription(string description)
        {
            string jsonstring = HttpContext.Session.GetString("testSession");
            if (jsonstring == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;


            UserMethods um = new UserMethods();
            int i = 0;
            string error = "";

            i = um.UpdateUserDescription(alias, description, out error);

            ViewBag.error = error;
            ViewBag.antal = i;

            if (error == "")
            {
                return RedirectToAction("ShowMyPage");
            }
            else
            {
                return View("UpdateViewFail");
            }


        }
    }
}
