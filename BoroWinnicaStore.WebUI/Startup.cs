using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoroWinnicaStore.WebUI.Startup))]
namespace BoroWinnicaStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
