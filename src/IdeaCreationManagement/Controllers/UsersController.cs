using System;
using System.Net.Http;
using System.Web.Mvc;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.Services;
using IdeaCreationManagement.ViewModels;

namespace IdeaCreationManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _users;

        public UsersController(UserService userService)
        {
            _users = userService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index(string msg)
        {
            var model = _users.GetAll();
            if (msg == "deleted")
            {
                ViewBag.Message = "Użytkownik został usunięty";
            }
            return View("Index", model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(string id, string msg)
        {
            var user = _users.GetUserDetails(id);
            if (user == null)
            {
                return new HttpNotFoundResult();
            }
            if (msg == "deassigned")
            {
                ViewBag.Message = "Przypisanie pracownika do projektu zostało usunięte";
            }

            return View(user);
        }

        [Authorize(Roles = "employee")]
        public ActionResult DetailsOnly2(string id)
        {
            var user = _users.GetUserDetails(id);
            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id)
        {
            return Details(id, null);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirm(string id)
        {
            string message = "";
            if (id != null)
            {
                _users.DeleteUser(id);
                message = "deleted";
            }
            return RedirectToAction("Index", new {msg = message});
        }

        [Authorize(Roles = "admin")]
        public ActionResult Deassign(string id, int projectId)
        {
            var model = _users.DeassignConfirmation(id, projectId);
            if (model.UserDetailsViewModel == null || model.Project == null)
            {
                return new HttpNotFoundResult();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeassignConfirm(string id, int projectId)
        {
            string message = "";
            var result = _users.Deassign(projectId);
            if (result)
            {
                message = "deassigned";
            }
            return RedirectToAction("Details", new {id = id, msg = message});
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            var model = _users.GetUserEditDetails(id);
            if (model == null)
            {
                return new HttpNotFoundResult();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id, [Bind] UserEditSubmitModel model)
        {
            if (!ModelState.IsValid)
            {
                return Edit(id);
            }
            _users.Update(id, model);
            return RedirectToAction("Edit", new {id});
        }
    }
}
