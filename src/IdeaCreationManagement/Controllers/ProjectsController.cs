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
            ProjectType projectType = type.ToLower() == "idea" ? ProjectType.Pomysł : ProjectType.Problem;
            ViewBag.CategoryId = new SelectList(_repo.GetProjectsCategories(projectType), "Id", "Name");
            ViewBag.ProjectType = type.ToLower() == "idea" ? "Dodaj Pomysł" : " Zgłoś problem";
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
                project.Type = type.ToLower() == "idea" ? ProjectType.Pomysł : ProjectType.Problem;
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
                    ViewBag.ErrorMessage = "Nie udało się dodać projektu do bazy. Spróbuj ponownie.";
                    return View("Error");
                }

            }

            return RedirectToAction("AllProjects");
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


        [Authorize(Roles = "employee")]
        public ActionResult AssignedProjects()
        {
            //TODO: zwrócenie projektów przydzielone do pracownika
            var userId = User.Identity.GetUserId();
            var assignedProjects = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.AssigneeId == userId);
            return View(assignedProjects.ToList());
        }

        [Authorize(Roles = "employee")]
        public ActionResult AssignedProjectsDetails(int? projectId)
        {
            if (projectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.Id == projectId).
                First();

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);

        }

        [Authorize(Roles = "employee,student")]
        public ActionResult GradedProjects()
        {
            //TODO: zwrócenie projektów przydzielone do pracownika
            var userId = User.Identity.GetUserId();
            var gradedProjects = db.Grades.
                Include(p => p.Project).
                Include(p => p.Rater).
                Where(p => p.RaterId == userId);

            return View(gradedProjects.ToList());
        }

        [Authorize(Roles = "employee,student")]
        public ActionResult GradedProjectsDetails(int? gradeId)
        {
            if (gradeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Grade grate = db.Grades.
                Include(p => p.Project).
                Include(p => p.Rater).
                Where(p => p.Id == gradeId).
                First();

            if (grate == null)
            {
                return HttpNotFound();
            }
            return View(grate);

        }

        [Authorize(Roles = "employee")]
        public ActionResult ChangeState(int? projectId)
        {
            if (projectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.Id == projectId).
                First();

            if (project.Type == IdeaCreationManagement.Models.ProjectType.Pomysł)
            {
                ViewBag.StateID = new SelectList(db.States.Where(p => p.Name == "zrealizowany" || p.Name == "niezrealizowany"), "Id", "Name");
            }
            else if (project.Type == IdeaCreationManagement.Models.ProjectType.Problem)
            {
                ViewBag.StateID = new SelectList(db.States.Where(p => p.Name == "rozwiązany" || p.Name == "nierozwiązany"), "Id", "Name");
            }


            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "employee")]
        public ActionResult ChangeState([Bind(Include = "Id,Time,Title,Description,AuthorId,AssigneeId,Type,AverageGrade,AverageUsefulness,AverageDifficulty,AverageIngenuity,StateId,CategoryId")] Project project)
        {
            if (ModelState.IsValid)
            {
                Alert newalert = new Alert {
                    TimeOfChange = DateTime.Now,
                    StateId = project.StateId,
                    AuthorOfChangeId = User.Identity.GetUserId(),
                    StudentRead = false,
                    EmployeeRead = false,
                    ProjectId = project.Id
                };

                db.Entry(project).State = EntityState.Modified;
                db.Alerts.Add(newalert);
                db.SaveChanges();
                return RedirectToAction("AssignedProjectsDetails", new { projectId = project.Id });
            }
            
            return View(project);

        }

        public ActionResult MyIdeas()
        {
            var userId = User.Identity.GetUserId();
            var type = ProjectType.Pomysł;
            var projects = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.AuthorId == userId).
                Where(p => (p.Type == type));
            return View(projects.ToList());
        }

        public ActionResult MyProblems()
        {
            var userId = User.Identity.GetUserId();
            var type = ProjectType.Problem;
            var projects = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.AuthorId == userId).
                Where(p => (p.Type == type));
            return View(projects.ToList());
        }

        public ActionResult AllProjects()
        {
            var projects = db.Projects.Include(p => p.Assignee).Include(p => p.Author).Include(p => p.Category)
                .Include(p => p.State).Include(p => p.Grades);
            ViewBag.Gradable = CheckGradability(projects);
            return View(projects.ToList());
        }

        private Dictionary<int, bool> CheckGradability(IEnumerable<Project> projects)
        {
            var dict = new Dictionary<int, bool>();
            var currentUserId = User.Identity.GetUserId();
            foreach (var project in projects)
            {
                if (project.Grades.Any(x => x.RaterId == currentUserId) || project.AuthorId == currentUserId 
                    || project.Type == ProjectType.Problem)
                {
                    dict.Add(project.Id, false);
                }
                else
                {
                    dict.Add(project.Id, true);
                }
            }
            return dict;
        }

        public ActionResult AllProjectsDetails(int? projectId)
        {
            if (projectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Project project = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Include(p => p.Grades).
                Where(p => p.Id == projectId).
                First();

            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gradable = CheckGradability(new[] { project });
            return View(project);

        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminAllProjects()
        {
            var projects = db.Projects.
                Include(p => p.Assignee).
                Include(p => p.Author).
                Include(p => p.Category).
                Include(p => p.State).
                Where(p => p.Type == ProjectType.Pomysł);

            return View(projects.ToList());
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
