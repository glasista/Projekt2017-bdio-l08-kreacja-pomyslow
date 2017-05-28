using System;
using System.Net.Http;
using System.Web.Mvc;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.Services;

namespace IdeaCreationManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _users;

        public UsersController(UserService userService)
        {
            _users = userService;
        }

        public ActionResult Index(string msg)
        {
            var model = _users.GetAll();
            if (msg == "deleted")
            {
                ViewBag.Message = "Użytkownik został usunięty";
            }
            return View("Index", model);
        }

        public ActionResult Details(string id)
        {
            var user = _users.GetUserDetails(id);
            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            return Details(id);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(string id)
        {
            if (id != null)
            {
                _users.DeleteUser(id);
            }
            return RedirectToAction("Index", new {msg = "deleted"});
        }
    }
}