using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdeaCreationManagement.Startup))]
namespace IdeaCreationManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
