using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using ProawarenessMeetupDemos.Hubs;
using ProawarenessMeetupDemos.Modules;

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

            GlobalHost.HubPipeline.AddModule(new ErrorHandlerModule());
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration { };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
