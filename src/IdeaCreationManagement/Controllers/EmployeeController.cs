using IdeaCreationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaCreationManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private AppContext _repository = new AppContext();

        // GET: Project
        public ActionResult List()
        {
            return View(_repository.Projects.ToArray());
        }

        public ActionResult Details(int projectId)
        {
            var model = _repository.Projects.Find(projectId);
            if (model == null)
            {
                return null;
            }
            else
            {
                return View(model);
            }
            //Project model = _repository.Projects.Where(p => p.Id == projectId).First();
            
        }
    }
}