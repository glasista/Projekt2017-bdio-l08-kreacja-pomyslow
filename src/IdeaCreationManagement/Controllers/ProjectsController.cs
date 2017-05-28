﻿using System;
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
    public class ProjectsController : Controller
    {
        private AppContext db = new AppContext();
        private readonly ProjectsRepository _repo = new ProjectsRepository();

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

        [Authorize]
        public ActionResult Add(string type)
        {
            if (type.IsNullOrWhiteSpace())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectType projectType = type.ToLower() == "idea" ? ProjectType.Idea : ProjectType.Problem;
            ViewBag.CategoryId = new SelectList(_repo.GetProjectsCategories(projectType), "Id", "Name");
            ViewBag.ProjectType = type.ToLower() == "idea" ? "Pomysł" : "Problem";
            return View("Create");
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken, Authorize]
        public ActionResult Add(string type, [Bind(Include = "Title,CategoryId,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> postedFiles = new List<HttpPostedFileBase>();
                project.AuthorId = User.Identity.GetUserId();
                project.Type = type.ToLower() == "idea" ? ProjectType.Idea : ProjectType.Problem;
                foreach (var upload in Request.Files.AllKeys)
                {
                    var file = Request.Files[upload];
                    if(file != null && file.ContentLength > 0)
                        postedFiles.Add(file);
                }
                try
                {
                    _repo.AddNewProject(project);
                    _repo.AddFilesIntoProject(project, postedFiles);
                    _repo.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Details", project);
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
        public ActionResult Edit([Bind(Include = "Id,Time,Title,Description,AuthorId,AssigneeId,Type,AverageGrade,AverageUsefulness,AverageDifficulty,AverageIngenuity,StateId,CategoryId")] Project project)
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
