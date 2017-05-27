using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaCreationManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppContext _ctx;

        public UsersController(AppContext ctx)
        {
            _ctx = ctx;
        }

        public static void ConfigureAutomapper(IMapperConfigurationExpression cfg)
        {
            // create dictionary mapping role ids to Polish names
            Dictionary<string, string> roles;
            using (var ctx = new AppContext())
            {
                roles = ctx.Roles.ToDictionary(x => x.Id, y => y.Name);
            }
            roles["employee"] = "pracownik";

            // set mappings
            cfg.CreateMap<IdentityUserRole, string>()
                .ProjectUsing(c => roles[c.RoleId]);
            cfg.CreateMap<User, ListUser>()
                .ForMember(x => x.RoleNames, c => c.MapFrom(x => x.Roles));
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