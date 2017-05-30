using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaCreationManagement.Services
{
    public class UserService
    {
        private readonly AppContext _ctx;
        private readonly ApplicationUserManager _userManager;

        public UserService(AppContext ctx, ApplicationUserManager userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
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
                .ForMember(x => x.Roles, c => c.MapFrom(x => x.Roles));
            cfg.CreateMap<User, UserDetails>()
                .ForMember(x => x.FieldOfStudy, c => c.MapFrom(x => x.FieldOfStudy != null ? x.FieldOfStudy.Name : null))
                .ForMember(x => x.OrganizationalUnit, c => c.MapFrom(x => x.OrganizationalUnit != null ? x.OrganizationalUnit.Name : null))
                .ForMember(x => x.Category, c => c.MapFrom(x => x.Category != null ? x.Category.Name : null))
                .ForMember(x => x.RoleNames, c => c.Ignore());
            cfg.CreateMap<Project, ListProject>()
                .ForMember(x => x.Author, c => c.MapFrom(x => x.Author != null ? x.Author.Name + " " + x.Author.Surname : "-"))
                .ForMember(x => x.Assignee, c => c.MapFrom(x => x.Assignee != null ? x.Assignee.Name + " " + x.Assignee.Surname : "-"))
                .ForMember(x => x.State, c => c.MapFrom(x => x.State.Name))
                .ForMember(x => x.Category, c => c.MapFrom(x => x.Category.Name));
        }

        public ICollection<ListUser> GetAll()
        {
            var users = _ctx.Users
                .ToList();
            return Mapper.Map<List<ListUser>>(users);
        }

        public UserDetailsViewModel GetUserDetails(string id)
        {
            var user = _ctx.Users
                .Where(x => x.Id == id)
                .Include(x => x.OrganizationalUnit)
                .Include(x => x.FieldOfStudy)
                .Include(x => x.Category)
                .ProjectTo<UserDetails>()
                .SingleOrDefault();
            if (user == null)
            {
                return null;
            }

            var created = _ctx.Projects
                .Where(x => x.AuthorId == user.Id)
                .Include(x => x.Assignee)
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.State)
                .ProjectTo<ListProject>()
                .ToList();
            var assigned = _ctx.Projects
                .Where(x => x.AssigneeId == user.Id)
                .ProjectTo<ListProject>()
                .ToList();

            return new UserDetailsViewModel()
            {
                Details = user,
                CreatedProjects = created,
                AssignedProjects = assigned,
            };
        }

        public void DeleteUser(string id)
        {
            var assigned = _ctx.Projects
                .Where(x => x.AssigneeId == id)
                .ToList();
            foreach (var project in assigned)
            {
                project.AssigneeId = null;
            }

            var created = _ctx.Projects
                .Where(x => x.AuthorId == id)
                .ToList();
            foreach (var project in created)
            {
                project.AuthorId = null;
            }



            var user = _userManager.FindById(id);
            _userManager.Delete(user);
            _ctx.SaveChanges();
        }

        public DeassignViewModel DeassignConfirmation(string id, int projectId)
        {
            var user = GetUserDetails(id);
            var project = _ctx.Projects.Find(projectId);
            return new DeassignViewModel()
            {
                Project = project,
                UserDetailsViewModel = user,
            };
        }

        public bool Deassign(int projectId)
        {
            var project = _ctx.Projects.Find(projectId);
            if (project != null)
            {
                project.AssigneeId = null;
                project.Assignee = null;
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }
    }
}