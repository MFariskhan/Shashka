using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shaska.Startup))]
namespace Shaska
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
