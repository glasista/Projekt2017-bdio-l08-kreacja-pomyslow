using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaCreationManagement.App_Start
{
    public class AutomapperConfig
    {
        private static Dictionary<string, string> Roles;

        public static void Configure()
        {
            using (var ctx = new AppContext())
            {
                Roles = ctx.Roles.
                    ToDictionary(x => x.Id, y => y.Name);
            }
            Roles["employee"] = "pracownik";

            Mapper.Initialize(Configuration);
        }

        private static void Configuration(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<IdentityUserRole, string>()
                .ProjectUsing(c => Roles[c.RoleId]);
            cfg.CreateMap<User, ListUser>()
                .ForMember(x => x.RoleNames, c => c.MapFrom(x => x.Roles));
        }
    }
}