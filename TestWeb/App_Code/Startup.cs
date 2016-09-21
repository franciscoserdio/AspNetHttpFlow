using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetErrorHandler.Startup))]
namespace AspNetErrorHandler
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
