using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TasteOfTheWorld.Startup))]
namespace TasteOfTheWorld
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
