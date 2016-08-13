using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ProawarenessMeetupDemos.Hubs
{
    public class UserLoginHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.Caller.OnConnectionIdReceived(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}