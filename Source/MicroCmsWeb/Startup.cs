using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicroCmsWeb.Startup))]
namespace MicroCmsWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
