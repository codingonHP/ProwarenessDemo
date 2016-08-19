using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(PersistentExamples.Startup))]

namespace PersistentExamples
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<HelloFromPersistant>("/signalr");

            GlobalHost.DependencyResolver.Register(typeof(IClockBroadCaster), () => {
                return new ClockBroadCaster();
            });

            GlobalHost.DependencyResolver.Register(typeof(HelloFromPersistant), () => {

                var clockBroadCaster = GlobalHost.DependencyResolver.GetService(typeof(IClockBroadCaster));
                return new HelloFromPersistant(clockBroadCaster as IClockBroadCaster);

            });
        }
    }
}
