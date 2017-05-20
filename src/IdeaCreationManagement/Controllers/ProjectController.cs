using IdeaCreationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaCreationManagement.Controllers
{
    public class ProjectController : Controller
    {
        private AppContext _repository = new AppContext();

        // GET: Project
        public ActionResult List()
        {
            return View(_repository.Projects.ToArray());
        }
    }
}