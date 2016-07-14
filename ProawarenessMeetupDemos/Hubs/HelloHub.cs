using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ProawarenessMeetupDemos.Hubs
{
    [HubName("HelloWorld")]
    public class HelloHub : Hub
    {
        private static int ClientsCount { get; set; }
        private static readonly object Lock = new object();

        public override Task OnConnected()
        {
            lock (Lock)
            {
                ++ClientsCount;
            }

            UpdateConnectedClientsCount();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            lock (Lock)
            {
                --ClientsCount;
            }
            UpdateConnectedClientsCount();
            return base.OnDisconnected(stopCalled);
        }

        private void UpdateConnectedClientsCount()
        {
            Clients.All.updateClientsConnectedCount(ClientsCount);
        }

        public void SayHelloWorld()
        {
            string message = "hello world";
            Clients.All.sayHello(message);
        }

       
    }

    public class HelloPersistance : PersistentConnection
    {
        
    }
}