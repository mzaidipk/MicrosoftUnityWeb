using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicrosoftUnityWeb.Startup))]
namespace MicrosoftUnityWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
