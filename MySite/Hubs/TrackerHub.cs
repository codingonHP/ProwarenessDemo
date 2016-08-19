using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System;

namespace MySite.Hubs
{
    public class TrackerHub : Hub
    {
        public override Task OnConnected()
        {
            string data = $"<b>{DateTime.Now}</b> : Conn ID : { Context.ConnectionId } =>User : {Context.User.Identity.Name}, URL: { Context.Request.Url }, Agent : {Context.Request.Headers["USER-AGENT"]}";
            Clients.All.trackClientsInfo(data);
            return base.OnConnected();
        }
    }
}