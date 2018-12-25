using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResidualCenter.Startup))]
namespace ResidualCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
