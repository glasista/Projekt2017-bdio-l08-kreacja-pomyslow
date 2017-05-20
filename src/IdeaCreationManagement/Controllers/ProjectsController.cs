using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaCreationManagement.Models;

namespace IdeaCreationManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Assignee).Include(p => p.Author).Include(p => p.Category).Include(p => p.State);
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
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
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.AssigneeId = new SelectList(db.Users, "Id", "Surname");
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Surname");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,AuthorId,AssigneeId,Type,AverageGrade,AverageUsefulness,AverageDifficulty,AverageIngenuity,StateId,CategoryId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssigneeId = new SelectList(db.Users, "Id", "Surname", project.AssigneeId);
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Surname", project.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", project.CategoryId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", project.StateId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AssigneeId = new SelectList(db.Users, "Id", "Surname", project.AssigneeId);
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Surname", project.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", project.CategoryId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", project.StateId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,AuthorId,AssigneeId,Type,AverageGrade,AverageUsefulness,AverageDifficulty,AverageIngenuity,StateId,CategoryId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssigneeId = new SelectList(db.Users, "Id", "Surname", project.AssigneeId);
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Surname", project.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", project.CategoryId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", project.StateId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
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
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
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
