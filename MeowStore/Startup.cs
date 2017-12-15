using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeowStore.Startup))]
namespace MeowStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
