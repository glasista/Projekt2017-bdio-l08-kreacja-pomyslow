using IdeaCreationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaCreationManagement.Controllers
{
    public class NavController : Controller
    {
        private AppContext db = new AppContext();

        //// GET: Nav
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public PartialViewResult Menu()
        {
            return PartialView();
        }
    }
}