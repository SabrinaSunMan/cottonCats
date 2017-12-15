using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BackMeow.Startup))]
namespace BackMeow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
