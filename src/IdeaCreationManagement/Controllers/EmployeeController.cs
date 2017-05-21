using IdeaCreationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace IdeaCreationManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Project
        public ActionResult AssignedProjects()
        {
            //TODO: zwrócenie projektów przydzielone do pracownika
            var assignedProjects = db.Projects.Include(p => p.Assignee).Include(p => p.Author).Include(p => p.Category).Include(p => p.State);
            return View(assignedProjects.ToList());
        }

        public ActionResult AssignedProjectsDetails(int? projectId)
        {
            if (projectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.
                Include(p => p.Assignee).Include(p => p.Author).Include(p => p.Category).Include(p => p.State).
                Where(p => p.Id == projectId).
                First();

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);

        }
    }
}