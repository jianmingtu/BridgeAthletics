using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Athletes.Startup))]
namespace Athletes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
