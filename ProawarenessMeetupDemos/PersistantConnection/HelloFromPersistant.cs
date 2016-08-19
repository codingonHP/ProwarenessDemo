using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace ProawarenessMeetupDemos.PersistantConnection
{
    public class HelloFromPersistant : PersistentConnection
    {
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            Connection.Send(connectionId, DateTime.UtcNow);
            return base.OnReceived(request, connectionId, data);
        }

    }
}