using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ProawarenessMeetupDemos.Utils;

namespace ProawarenessMeetupDemos.Hubs
{
    public class UserLoginHub : Hub
    {
        ClientManager _clientManager = new ClientManager();

        public override Task OnConnected()
        {
            _clientManager.AddToActiveLoggedInClientList(Context.User.Identity.Name, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            _clientManager.AddToActiveLoggedInClientList(Context.User.Identity.Name, Context.ConnectionId);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _clientManager.RemoveUserFromClientsList(Context.User.Identity.Name, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }

    //public class ConnectionFactory : IUserIdProvider
    //{
    //    public string GetUserId(IRequest request)
    //    {
    //        Cookie value;
    //        if (request.Cookies.TryGetValue("srconnectionid", out value))
    //        {
    //            return request.Cookies["srconnectionid"].ToString();
    //        }

    //        var connID = Guid.NewGuid().ToString();
    //        return connID;
    //    }
    //}
}