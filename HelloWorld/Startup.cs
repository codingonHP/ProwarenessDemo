using Microsoft.Owin;
using Owin;
using MyWebApplication;
using Microsoft.Owin.Helpers;

namespace MyWebApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/chat", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR();
            });
        }

    }
}