using Microsoft.AspNetCore.Mvc;
using HaikuLab3.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HaikuLab3.Controllers
{
    public class UserController : Controller
    {

        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult AddPic()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddPic(IList<IFormFile> files)
        {
            string filename = "";

            foreach (IFormFile source in files)
            {
                filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                    await source.CopyToAsync(output);
            }

            string photo = filename;

            string jsonstring = HttpContext.Session.GetString("testSession");

            string alias = JsonConvert.DeserializeObject<string>(jsonstring);

            UserMethods um = new UserMethods();

            string error = "";

            um.UpdateUserPhoto(alias, photo, out error);

            ViewBag.error = error;

            if (error == "")
            {
                return RedirectToAction("ShowMyPage");
            }
            else
            {
                return View("UpdateViewFail");
            }

            //return this.View();
        }



        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);



            return filename;
        }



        private string GetPathAndFilename(string filename)
        {

            return this._hostEnvironment.WebRootPath + "/uploads/" + filename;
        }

        [HttpGet]
        public IActionResult InsertUser()
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

        public IActionResult InsertUserFail()
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
                string sesstest = "test av sessionsvariabel";
                //string photo = ud.Us_Photo;

                i = um.InsertUser(ud, out error);

                ViewBag.error = error;
                ViewBag.antal = i;

                if (error == "")
                {
                    string u = JsonConvert.SerializeObject(alias);
                    HttpContext.Session.SetString("testSession", u);
                    
                    //return View("InsertUser");
                    return RedirectToAction("InsertUser");
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
                return RedirectToAction("LogInUser");
            }
            string alias = JsonConvert.DeserializeObject<string>(jsonstring);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;

            HaikuListMethods hlm = new HaikuListMethods();
            UserMethods um = new UserMethods();
            ViewModelUserHaiku vmuh = new ViewModelUserHaiku
            {
                HaikuListDetailList = hlm.SelectHaikuListForUser(out string errormsg, alias),
                UserDetailList = um.SelectUserList(out string errormsg2, alias),
                userDetail = um.SelectUser(out string errormsg3, alias)
            };
            TempData["Test"] = alias;
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
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
            //string jsonstring3 = HttpContext.Session.GetString("testSession3");
            //string sesstest = JsonConvert.DeserializeObject<string>(jsonstring3);
            //string alias = uu.Us_Alias;
            ViewBag.jsonstring = jsonstring;
            TempData["Test"] = alias;
            //TempData["Test2"] = sesstest;
            return View();

        }

        [HttpPost]
        public IActionResult EditEmail(string mail)
        {
            if (ModelState.IsValid)
            {
                //string email = ud.Us_Email;
                string jsonstring = HttpContext.Session.GetString("testSession");
                if (jsonstring == null)
                {
                    return RedirectToAction("Home", "Start");
                }
                string alias = JsonConvert.DeserializeObject<string>(jsonstring);
                //string alias = uu.Us_Alias;
                ViewBag.jsonstring = jsonstring;


                UserMethods um = new UserMethods();
                int i = 0;
                string error = "";

                i = um.UpdateUserEmail(alias, mail, out error);

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
            return RedirectToAction("EditEmail");

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

        [HttpGet]
        public IActionResult LogOutUser()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Home", "Start");
            
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
                UserDetailList = um.SelectUserList(out string errormsg2, id),
                userDetail = um.SelectUser(out string errormsg3, id)
                
            };

            TempData["user"] = id;

            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
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
