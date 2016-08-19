using Microsoft.AspNet.SignalR;
using System;
using System.Threading;

namespace PersistentExamples
{
    public class ClockBroadCaster : IClockBroadCaster
    {
        private static Timer _timer;

        private void Callback(object state)
        {
            var ctx = GlobalHost.ConnectionManager.GetConnectionContext<HelloFromPersistant>();
            ctx.Connection.Broadcast(DateTime.Now);
        }

        public void Start()
        {
            if (_timer == null)
            {
                _timer = new Timer(Callback, null, TimeSpan.Zero.Seconds, TimeSpan.FromSeconds(1).Seconds);
            }
        }

        public void Stop()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Dispose();
            _timer = null;
        }
    }
}