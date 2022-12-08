using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReelflicsWebsite.Startup))]
namespace ReelflicsWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
