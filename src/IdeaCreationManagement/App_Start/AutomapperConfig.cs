using AutoMapper;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.ViewModels;

namespace IdeaCreationManagement.App_Start
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(Configuration);
        }

        private static void Configuration(IMapperConfigurationExpression cfg)
        {
        }
    }
}