using System.Collections.Generic;
using System.Web.Mvc;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.ViewModels;

namespace IdeaCreationManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppContext _ctx;

        public UsersController(AppContext ctx)
        {
            _ctx = ctx;
        }

        public ActionResult Index()
        {
            var a = new ListUser()
            {
                Email = "a@a.pl",
                EmailConfirmed = false,
                Id = "abc123",
                Name = "pawel",
                Surname = "pietrasz",
                Roles = "admin",
            };

            var b = new ListUser()
            {
                Email = "a@a.pl",
                EmailConfirmed = false,
                Id = "abc1234",
                Name = "pawel",
                Surname = "pietrasz",
                Roles = "pracownik",
            };
            return View(new List<ListUser>(){a, b});
        }
    }
}