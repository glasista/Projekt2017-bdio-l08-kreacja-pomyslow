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

       
       [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();

        }
        [Authorize(Roles = "admin")]
        public ActionResult ViewProjects()
        {

            var projects = db.Projects.Include(p => p.Assignee).Include(p => p.Author).Include(p => p.Category).Include(p => p.State);
            return View(projects.ToList());
          
        }
        [Authorize(Roles = "admin")]
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
            ViewBag.ProjectID = project.Id;
            ViewBag.Projects = project.Title;
            var employee = db.Users.Include(p => p.OrganizationalUnit).Include(p => p.Category).Where(p => p.CategoryId == project.CategoryId);
            // .Where(c => c.Category.Name == project.Category.Name);
            return View(employee.ToList());
        }
        [Authorize(Roles = "admin")]
        public ActionResult SetEmployee(string id_e , int? id_p)
        {
            if (id_e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(id_p == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User emp = db.Users.Include(p => p.OrganizationalUnit).Include(p => p.Category).Where(p => p.Id == id_e).First();

            Project project = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.Id == id_p).
                First();

            project.AssigneeId = emp.Id;
            project.State.Name = "przydzielony";
            db.SaveChanges();   
            return RedirectToAction("ViewProjects");
        }
        [Authorize(Roles = "admin")]
        public ActionResult RejectProject(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).Where(p => p.Id == id).First();


            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);

        }
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("RejectProject")]
        [ValidateAntiForgeryToken]
        public ActionResult RejectConfirmed(int? id)
        {
            Project project = db.Projects.Find(id);
            project.State = new State() { Name = "odrzucony" };
            project.AssigneeId = null;
            db.SaveChanges();
            return RedirectToAction("ViewProjects");
        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).Where(p => p.Id == id).First();

            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("ViewProjects");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}