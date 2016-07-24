using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ProawarenessMeetupDemos.Hubs
{
    public class User
    {
        public string UserName { get; set; }
        public HashSet<string> ConnectionIds { get; set; }

    }

    public class ChatGroup
    {
        public int MemberCount { get; set; }
        public string GroupName { get; set; }
        public string ConnectionId { get; set; }
        public string Owner { get; set; }
    }

    public interface IChatClient
    {
        void UserConnected(string userName);
        void UserDisconnected(string userName);
        void Received(object message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        public static readonly ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new User {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }

            Clients.AllExcept(user.ConnectionIds.ToArray()).UserConnected(userName);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;
            User user;

            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(c => c == connectionId);
                    if (!user.ConnectionIds.Any())
                    {
                        User removedUser;
                        Users.TryRemove(userName, out removedUser);

                        if (removedUser != null)
                        {
                            Clients.Others.UserDisconnected(userName);
                        }
                    }

                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public void Send(string message)
        {
            string sender = Context.User.Identity.Name;
            Clients.All.Received(new
            {
                sender = sender,
                message = message,
                isPrivate = false
            });
        }

        public void Send(string message, string to)
        {
            User receiver;
            if (Users.TryGetValue(to, out receiver))
            {
                User sender = GetUser(Context.User.Identity.Name);
                if (sender != null)
                {
                    IEnumerable<string> allReceivers;
                    lock (sender.ConnectionIds)
                    {
                        allReceivers = receiver.ConnectionIds.Concat(sender.ConnectionIds);
                    }

                    foreach (var cid in allReceivers)
                    {
                        Clients.Client(cid).Received(new
                        {
                            sender = sender.UserName,
                            message = message,
                            isPrivate = true
                        });
                    }
                }
            }
        }

        private User GetUser(string username)
        {
            User user;
            Users.TryGetValue(username, out user);
            return user;
        }
    }
}