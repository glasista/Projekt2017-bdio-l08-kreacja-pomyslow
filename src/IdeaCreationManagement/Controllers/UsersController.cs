using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
            var users = _ctx.Users
                .ToList();
            var model = Mapper.Map<List<ListUser>>(users);
            return View(model);
        }
    }
}