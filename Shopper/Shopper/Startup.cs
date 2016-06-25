using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shopper.Startup))]
namespace Shopper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
