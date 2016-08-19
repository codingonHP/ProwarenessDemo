using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UserTracking.Startup))]

namespace UserTracking
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
