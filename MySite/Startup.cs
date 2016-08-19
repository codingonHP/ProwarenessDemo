using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(MySite.Startup))]

namespace MySite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/realtime", (action) =>
            {
                action.UseCors(CorsOptions.AllowAll);
                action.RunSignalR(new Microsoft.AspNet.SignalR.HubConfiguration {
                    EnableJSONP = true,
                    EnableJavaScriptProxies = true
                });
            });
        }
    }
}
