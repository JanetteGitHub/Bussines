using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bussines.Backend.Startup))]
namespace Bussines.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
