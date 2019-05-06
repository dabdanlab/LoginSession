using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginSession.Startup))]
namespace LoginSession
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
