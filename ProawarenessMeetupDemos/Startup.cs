using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using ProawarenessMeetupDemos.Hubs;

[assembly: OwinStartupAttribute(typeof(ProawarenessMeetupDemos.Startup))]
namespace ProawarenessMeetupDemos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //var IdProvider = new ConnectionFactory();
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => IdProvider);

            app.MapSignalR();
        }
    }
}
