using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IdeaCreationManagement.Controllers;
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
            Mapper.Initialize(Configuration);
        }

        private static void Configuration(IMapperConfigurationExpression cfg)
        {
            UsersController.ConfigureAutomapper(cfg);
        }
    }
}