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

namespace IdeaCreationManagement.Controllers
{
    public class GradesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Grades
        public ActionResult Index(ProjectType type = ProjectType.Pomysł)
        {



            return View(db.Grades.ToList());
        }

        public ActionResult AllGradesDetails(int? projectId)
        {
            if (projectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var selected = db.Grades.
                Include(c => c.Rater).
                Where(c => c.ProjectId == projectId);
              
            return View(selected.ToList());
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create( int id)
        {
            
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id,[Bind(Include = "Time,UsefulnessValue,DifficultyValue,Ingenuity")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.ProjectId =id;
                grade.RaterId = User.Identity.GetUserId();
                grade.Time = DateTime.Now;
                grade.AverageGrade = (grade.DifficultyValue + grade.Ingenuity + grade.UsefulnessValue) / 3;
                db.Grades.Add(grade);
                db.SaveChanges();

               
                var mediumgrade = db.Grades.Where(g => g.ProjectId == id).ToList();

                db.Projects.Find(id).AverageIngenuity = mediumgrade.Sum(g => g.Ingenuity) / mediumgrade.Count;
                db.Projects.Find(id).AverageDifficulty = mediumgrade.Sum(g => g.DifficultyValue) / mediumgrade.Count;
                db.Projects.Find(id).AverageUsefulness = mediumgrade.Sum(g => g.UsefulnessValue) / mediumgrade.Count;
                db.Projects.Find(id).AverageGrade = mediumgrade.Sum(g => g.AverageGrade) / mediumgrade.Count;
                db.SaveChanges();
                return RedirectToAction("AllProjectsDetails", "Projects", new { ProjectId = grade.ProjectId });
            }

            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,UsefulnessValue,DifficultyValue,Ingenuity")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
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
