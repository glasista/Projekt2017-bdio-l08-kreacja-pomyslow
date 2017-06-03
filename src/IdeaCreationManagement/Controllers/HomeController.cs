using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaCreationManagement.Models;
using Microsoft.AspNet.Identity;

namespace IdeaCreationManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
      /*  public string GetInfo()
        {
            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
          
            using (var db = new AppContext())
            { 
               
            var pass = db.Users.FirstOrDefault(p => p.UserName == email);
                string xx = "Qwerty_123";
               

                string passHash2 = System.Web.Helpers.Crypto.SHA256(xx);              
                var verif = VerifyHashedPassword(pass.PasswordHashed, passHash2);
                return pass.Email+"<p>Эл. адрес: " + xx + "</p><p>Пол:" +verif +"</p><p> Возраст:" +pass.PasswordHashed+ "</p>";
            }            
        }*/
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword.Equals(providedPassword))
                return PasswordVerificationResult.Success;
            else return PasswordVerificationResult.Failed;
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}