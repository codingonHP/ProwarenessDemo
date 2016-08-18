using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Admin.Startup))]
namespace Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR("/chatapp/proxy/js",new Microsoft.AspNet.SignalR.HubConfiguration {
                EnableDetailedErrors = true,
                EnableJSONP = true
            });
            ConfigureAuth(app);
        }
    }
}
