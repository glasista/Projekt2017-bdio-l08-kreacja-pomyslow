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
    public class FilesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Files
        public ActionResult FileIndex(int? projectId)
        {
            var files = db.Files.
                Include(f => f.Project).
                Where(f => f.ProjectId == projectId);
            return View(files.ToList());
        }

        // GET: Files/Details/5
        public ActionResult Details(int? fileId)
        {
            if (fileId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(fileId);

            if (file == null)
            {
                return HttpNotFound();
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", file.Name));
            Response.AddHeader("Content-Length", file.Size.ToString());
            Response.BinaryWrite(file.Content);
            Response.End();

            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title");
            return View();
        }

        // POST: Files/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DataType,Content,Size,ProjectId")] File file)
        {
            if (ModelState.IsValid)
            {
                db.Files.Add(file);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", file.ProjectId);
            return View(file);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", file.ProjectId);
            return View(file);
        }

        // POST: Files/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DataType,Content,Size,ProjectId")] File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", file.ProjectId);
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            File file = db.Files.Find(id);
            db.Files.Remove(file);
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
