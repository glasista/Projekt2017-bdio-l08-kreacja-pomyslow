using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.Repositories;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;


namespace IdeaCreationManagement.Controllers
{
    public class AdminController : Controller
    {
        private AppContext db = new AppContext();

        // GET: 
        [Authorize(Roles = "admin")]

        public ActionResult Index()
        {
            return View();

        }

        public ActionResult ViewProjects()
        {

            var projects = db.Projects.Include(p => p.Assignee).Include(p => p.Author).Include(p => p.Category).Include(p => p.State);
            return View(projects.ToList());
          
        }
        public ActionResult ChooseEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Projects = project.Title;
            var employee = db.Users.Include(p => p.OrganizationalUnit).Include(p => p.Category).Where(p => p.Category == project.Category);
            return View(employee);
        }
    }


}