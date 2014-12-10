using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpringSoftware.Web.Startup))]
namespace SpringSoftware.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
