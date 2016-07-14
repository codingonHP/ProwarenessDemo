using System;
using System.Threading;
using Microsoft.AspNet.SignalR;

namespace ProawarenessMeetupDemos.Hubs
{
    public class TrackModel
    {
        public string ConnectionId { get; set; }
        public string XPos { get; set; }
        public string YPos { get; set; }
    }

    public class BroadCaster
    {
        private static readonly Lazy<BroadCaster> _instance = new Lazy<BroadCaster>(() => new BroadCaster());
        private readonly TimeSpan _broadcastInterval = TimeSpan.FromMilliseconds(40);
        private readonly IHubContext _hubContext;
        private Timer _broadcastLoop;
        private TrackModel _model;
        private bool _trackInfoUpdated;

        public static BroadCaster Instance => _instance.Value;

        private BroadCaster()
        {
            // Save our hub context so we can easily use it 
            // to send to its connected clients
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<ReaderTrackerHub>();
            _model = new TrackModel();
            _trackInfoUpdated = false;

            // Start the broadcast loop
            _broadcastLoop = new Timer(BroadcastTrackInfo, null, _broadcastInterval, _broadcastInterval);
        }

        public void BroadcastTrackInfo(object state)
        {
            // No need to send anything if our model hasn't changed
            if (_trackInfoUpdated)
            {
                // This is how we can access the Clients property 
                // in a static hub method or outside of the hub entirely
                _hubContext.Clients.AllExcept(_model.ConnectionId).notifyClients(_model);
                _trackInfoUpdated = false;
            }
        }

        public void UpdateTrackInfo(TrackModel clientModel)
        {
            _model = clientModel;
            _trackInfoUpdated = true;
        }

        
    }

    public class ReaderTrackerHub : Hub
    {
        // Is set via the constructor on each creation
        private readonly BroadCaster _broadcaster;
        public ReaderTrackerHub(): this(BroadCaster.Instance)
	    {
        }
        public ReaderTrackerHub(BroadCaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public void UpdateTrackInfo(string x, string y)
        {
            var connection = Context.ConnectionId;
            TrackModel model = new TrackModel
            {
                ConnectionId = connection,
                XPos = x,
                YPos = y
            };

            //Clients.AllExcept(connection).notifyClients(model);

            // Update the shape model within our broadcaster
            _broadcaster.UpdateTrackInfo(model);
        }
    }
}