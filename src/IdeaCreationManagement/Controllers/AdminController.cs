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
        public ActionResult SetEmployee(string id_e, int? id_p)
        {
            if (id_e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id_p == null)
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
            Alert newalert = new Alert
            {
                TimeOfChange = DateTime.Now,
                StateId = project.StateId,
                AuthorOfChangeId = User.Identity.GetUserId(),
                StudentRead = false,
                EmployeeRead = false,
                ProjectId = project.Id
            };
            db.Alerts.Add(newalert);
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

        [Authorize(Roles = "admin")]
        public ActionResult ConfirmEmployee()
        {
            var roleId = db.Roles.Where(x => x.Name.Equals("employee")).Select(y => y.Id).FirstOrDefault();
            var employee = db.Users.Where(x => x.EmailConfirmed == false).Where(x => x.Roles.Any(y => y.RoleId.Equals(roleId))).ToList();
            return View(employee);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ConfirmEmployeeAcc(string id_e)
        {
            if (id_e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User employee = db.Users.Find(id_e);
            employee.EmailConfirmed = true;
            db.SaveChanges();
            return RedirectToAction("AddCategoryToEmp",new {id_e =id_e });
        }

        [Authorize(Roles = "admin")]
        public ActionResult NegativeEmployeeAcc(string id_e)
        {
            if (id_e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User employee = db.Users.Find(id_e);
            db.Users.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("ConfirmEmployee");
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddCategoryToEmp(string id_e)
        {
           // var employee = db.Users.Find(id_e);
            var category = db.Categories.ToList();
            ViewBag.id_e = id_e;
            ViewBag.categories = category;
            return View(ViewBag);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AssignCategory(string id_e,int? id_c)
        {
            if (id_e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User employee = db.Users.Find(id_e);
            //Category cat = db.Categories.Find(id_c);
            employee.CategoryId = id_c;
            db.SaveChanges();
            return RedirectToAction("ConfirmEmployee");
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