using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ProawarenessMeetupDemos.Hubs
{
    public class DrawingHub : Hub
    {
        public Task BroadcastPoint(float x, float y)
        {
            return Clients.Others.drawPoints(x, y, Clients.Caller.color);
        }

        public Task BroadcastClear()
        {
            return Clients.All.clear();
        }
    }
}