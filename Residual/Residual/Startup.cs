using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Residual.Startup))]
namespace Residual
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
