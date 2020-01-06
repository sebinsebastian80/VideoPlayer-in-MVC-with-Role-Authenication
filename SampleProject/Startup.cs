using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleProject.Startup))]
namespace SampleProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
