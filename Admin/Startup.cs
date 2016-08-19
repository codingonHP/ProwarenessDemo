using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(Admin.Startup))]
namespace Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Map("/signalr", map =>
            //{
            //    map.UseCors(CorsOptions.AllowAll);
            //    map.RunSignalR(new Microsoft.AspNet.SignalR.HubConfiguration {
            //        EnableJSONP = true
            //    });
            //});

            //app.MapSignalR("/chatapp/proxy/js",new Microsoft.AspNet.SignalR.HubConfiguration {
            //    EnableDetailedErrors = true,
            //    EnableJSONP = true
            //});

            ConfigureAuth(app);
        }
    }
}
