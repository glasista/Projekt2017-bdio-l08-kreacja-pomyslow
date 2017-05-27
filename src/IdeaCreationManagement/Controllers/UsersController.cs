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

        public ActionResult Index()
        {
            var model = _users.GetAll();
            return View(model);
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
    }
}