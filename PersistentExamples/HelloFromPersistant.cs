using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace PersistentExamples
{
    public  class HelloFromPersistant  : PersistentConnection
    {
        IClockBroadCaster _clockBroadCaster;

        public HelloFromPersistant(IClockBroadCaster clockBroadCaster)
        {
            _clockBroadCaster = clockBroadCaster;
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            if (data == "start")
            {
                _clockBroadCaster.Start();
            }
            else
            {
                _clockBroadCaster.Stop();
            }

            return base.OnReceived(request, connectionId, data);
        }
    }
}