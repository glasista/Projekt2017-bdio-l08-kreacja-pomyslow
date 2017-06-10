using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaCreationManagement.Models;
using Microsoft.AspNet.Identity;
using System.Runtime.Remoting.Contexts;
using Microsoft.Ajax.Utilities;

namespace IdeaCreationManagement.Controllers
{
    public class AlertsController : Controller
    {
        private AppContext context = new AppContext();

        // GET: Alerts
        public ActionResult Index()
        {
            var alerts = context.Alerts.Include(a => a.AuthorOfChange).Include(a => a.Project).Include(a => a.State);
            if (User.IsInRole("student"))
            {
                var userId = User.Identity.GetUserId();
                var user = context.Users.Include(x => x.CreateProjects).Single(x => x.Id == userId);
                var list = new List<Alert>();
                foreach (var project in user.CreateProjects)
                {
                        var alerty = context.Alerts.
                        Include(a => a.AuthorOfChange).
                        Include(a => a.Project).
                        Include(a => a.State).
                        Where(x => x.ProjectId == project.Id && x.StudentRead == false);
                        list.AddRange(alerty);
                }
                return View(list);
            }
            if (User.IsInRole("employee"))
            {
                var userId = User.Identity.GetUserId();
                var user = context.Users.Include(x => x.AssignedProjects).Single(x => x.Id == userId);
                var list = new List<Alert>();
                foreach (var project in user.AssignedProjects)
                {
                    var alerty = context.Alerts.
                        Include(a => a.AuthorOfChange).
                        Include(a => a.Project).
                        Include(a => a.State).
                        Where(x => x.ProjectId == project.Id && x.EmployeeRead == false);
                    list.AddRange(alerty);
                }
                return View(list);
            }
            return View(alerts.ToList());
        }

        // GET: Alerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = context.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        public ActionResult AlertDetails(int? alertId)
        {
            if (alertId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = context.Alerts.
                Include(a => a.AuthorOfChange).
                Include(a => a.Project).
                Include(a => a.State).
                Where(a => a.Id == alertId).
                First();
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // GET: Alerts/Create
        public ActionResult Create()
        {
            ViewBag.AuthorOfChangeId = new SelectList(context.Users, "Id", "Name");
            ViewBag.ProjectId = new SelectList(context.Projects, "Id", "Title");
            ViewBag.StateId = new SelectList(context.States, "Id", "Name");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeOfChange,StateId,AuthorOfChangeId,StudentRead,EmployeeRead,ProjectId")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                context.Alerts.Add(alert);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorOfChangeId = new SelectList(context.Users, "Id", "Name", alert.AuthorOfChangeId);
            ViewBag.ProjectId = new SelectList(context.Projects, "Id", "Title", alert.ProjectId);
            ViewBag.StateId = new SelectList(context.States, "Id", "Name", alert.StateId);
            return View(alert);
        }

        // GET: Alerts/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = context.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: Alerts/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alert alert = context.Alerts.Find(id);
            context.Alerts.Remove(alert);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
