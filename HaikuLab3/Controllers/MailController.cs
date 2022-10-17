using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
//using System.Web.Mvc;

namespace HaikuLab3.Controllers
{
    public class MailController : Controller
    {
        // GET: /SendMailer/   
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.MailDetail _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("skrivenhaiku@gmail.com");
                mail.From = new MailAddress("skrivenhaiku@gmail.com");
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body + ". \nAvsändare: " + _objModelMail.CC;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("skrivenhaiku@gmail.com", "pyhfhxltdoncdvbh"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return RedirectToAction("Home", "Start");
            }
            else
            {
                return View();
            }
        }
    }
}
