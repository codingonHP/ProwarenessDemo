using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace ProawarenessMeetupDemos.Hubs
{
    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonIgnore]
        public string ConnectionId { get; set; }
    }

    public class Manager
    {
        private static readonly Lazy<Manager> _instance = new Lazy<Manager>(() => new Manager());
        internal static Manager Instance { get { return _instance.Value; } }

        private bool cold = true;

        private Manager()
        {

        }
       
        internal void NotifyAllClients(Location clientLocation)
        {
            GetHubContext().Clients.AllExcept(clientLocation.ConnectionId).OnClientConnect(clientLocation);
        }

        private IHubContext GetHubContext()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<GeoLocationHub>();
            return hubContext;
        }
    }

    public class GeoLocationHub : Hub
    {
        Manager _manager;
        public GeoLocationHub() : this(Manager.Instance)
        {

        }

        public GeoLocationHub(Manager manager)
        {
            _manager = manager;
        }

        public override Task OnConnected()
        {
            var request = Context.Request;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public void UpdateNetwork(Location clientLocation)
        {
            //Testing code : points to somewhere in Peru
            //clientLocation.Latitude = -12.043333;
            //clientLocation.Longitude = -77.028333;
            
            clientLocation.ConnectionId = Context.ConnectionId;
            _manager.NotifyAllClients(clientLocation);
        }
    }
}