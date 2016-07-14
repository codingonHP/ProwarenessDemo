using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProawarenessMeetupDemos.Startup))]
namespace ProawarenessMeetupDemos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
