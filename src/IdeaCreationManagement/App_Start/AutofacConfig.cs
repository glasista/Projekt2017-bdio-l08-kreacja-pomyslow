using System.Data.Entity;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using IdeaCreationManagement.Models;
using IdeaCreationManagement.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            builder.RegisterType<AppContext>().AsSelf().As<DbContext>().InstancePerRequest();
            builder.RegisterType<UserService>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
        }
    }
}