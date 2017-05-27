using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.Services;

namespace IdeaCreationManagement
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Register(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AppContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserService>().AsSelf().InstancePerRequest();
        }
    }
}