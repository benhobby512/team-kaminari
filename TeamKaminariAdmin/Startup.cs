using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamKaminariAdmin.Startup))]
namespace TeamKaminariAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
