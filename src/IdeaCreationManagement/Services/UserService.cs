using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
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
            var employeeId = roles.SingleOrDefault(x => x.Value == "employee").Key;
            roles[employeeId] = "pracownik";

            // set mappings
            cfg.CreateMap<IdentityUserRole, string>()
                .ProjectUsing(c => roles[c.RoleId]);
            cfg.CreateMap<User, ListUser>()
                .ForMember(x => x.Roles, c => c.MapFrom(x => x.Roles))
                .ForMember(x => x.Category, c => c.MapFrom(x => x.Category != null ? x.Category.Name : "-"));
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
            cfg.CreateMap<Category, SelectListItem>()
                .ForMember(x => x.Value, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.Text, c => c.MapFrom(x => x.Name));
            cfg.CreateMap<FieldOfStudy, SelectListItem>()
                .ForMember(x => x.Value, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.Text, c => c.MapFrom(x => x.Name));
            cfg.CreateMap<OrganizationalUnit, SelectListItem>()
                .ForMember(x => x.Value, c => c.MapFrom(x => x.Id))
                .ForMember(x => x.Text, c => c.MapFrom(x => x.Name));
            cfg.CreateMap<User, UserEditViewModel>();
            cfg.CreateMap<UserEditSubmitModel, User>();
        }

        public ICollection<ListUser> GetAll()
        {
            var users = _ctx.Users
                .Include(x => x.Category)
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
                .SingleOrDefault();
            if (user == null)
            {
                return null;
            }

            var userModel = Mapper.Map<UserDetails>(user);

            var created = _ctx.Projects
                .Where(x => x.AuthorId == user.Id)
                .Include(x => x.Assignee)
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.State)
                .ProjectTo<ListProject>()
                .ToList();
            var assigned = GetAssignedProjects(id);

            return new UserDetailsViewModel()
            {
                Details = userModel,
                CreatedProjects = created,
                AssignedProjects = assigned,
            };
        }

        private List<ListProject> GetAssignedProjects(string userId)
        {
            return _ctx.Projects
                .Where(x => x.AssigneeId == userId)
                .ProjectTo<ListProject>()
                .ToList();
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

        public UserEditViewModel GetUserEditDetails(string id)
        {
            var user = ((IQueryable<User>) _ctx.Users)
                .Include(x => x.Roles)
                .SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            var model = Mapper.Map<UserEditViewModel>(user);

            model.Categories = _ctx.Categories.ProjectTo<SelectListItem>().ToList();
            model.FieldsOfStudy = _ctx.FieldsOfStudies.ProjectTo<SelectListItem>().ToList();
            model.OrganizationalUnits = _ctx.OrganizationalUnits.ProjectTo<SelectListItem>().ToList();

            var roles = _ctx.Roles.ToDictionary(x => x.Name, y => y.Id);
            model.IsStudent = user.Roles.Any(x => x.RoleId == roles["student"]);
            model.IsEmployee = user.Roles.Any(x => x.RoleId == roles["employee"]);
            model.AssignedProjects = GetAssignedProjects(id);

            return model;
        }

        public bool Update(string id, UserEditSubmitModel model)
        {
            var user = ((IQueryable<User>) _ctx.Users)
                .Include(x => x.Roles)
                .SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return false;
            }

            var roles = _ctx.Roles.ToDictionary(x => x.Name, y => y.Id);

            if (user.Roles.All(x => x.RoleId != roles["student"]))
            {
                model.FieldOfStudyId = null;
                model.StudentNumberView = null;
            }

            if (user.Roles.All(x => x.RoleId != roles["employee"]))
            {
                model.CategoryId = null;
                model.OrganizationalUnitId = null;
            }

            Mapper.Map(model, user);
            _ctx.SaveChanges();
            return true;
        }
    }
}