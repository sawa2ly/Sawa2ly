using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sawa2ly.Startup))]
namespace Sawa2ly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
