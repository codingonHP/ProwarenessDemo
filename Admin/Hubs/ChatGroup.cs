using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Admin.Hubs
{
    public class ChatGroup : Hub
    {
        static object _lock = new object();
        static int _clientsCount = 0;
        static ConcurrentDictionary<string, List<string>> _userGroups = new ConcurrentDictionary<string, List<string>>();

        public override Task OnConnected()
        {
            lock (_lock)
            {
                ++_clientsCount;
            }

            Clients.All.updateUserCount(_clientsCount);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            lock (_lock)
            {
                --_clientsCount;

            }

            Clients.All.updateUserCount(_clientsCount);
            return base.OnDisconnected(stopCalled);
        }

        public Task<bool> AddUserToGroup(string groupName)
        {
            AddUserToNewGroup(groupName);
            return Groups.Add(Context.ConnectionId, groupName).ContinueWith(_ =>
            {
                return true;
            });
        }

        private bool UserExists(string connectionId)
        {
            return _userGroups.ContainsKey(connectionId);
        }

        private void AddUserToNewGroup(string groupName)
        {
            if (UserExists(Context.ConnectionId))
            {
                var memberOfGroups = _userGroups[Context.ConnectionId];
                memberOfGroups.Add(groupName);
            }
            else
            {
                var memberOfGroups = new List<string>();
                memberOfGroups.Add(groupName);

                _userGroups.TryAdd(Context.ConnectionId, memberOfGroups);
            }
        }

        public Task<bool> RemoveUserFromGroup(string groupName)
        {
            RemoveUserFromGroupList(groupName);
            return Groups.Remove(Context.ConnectionId, groupName).ContinueWith(_ =>
            {
                Clients.Caller.UpdateGroupList(groupName, "remove");
                return true;
            });
        }

        private void RemoveUserFromGroupList(string groupName)
        {
            if (UserExists(Context.ConnectionId))
            {
                var memberOfGroups = _userGroups[Context.ConnectionId];
                memberOfGroups.Remove(groupName);
            }

        }

        public void CreateRoom(string roomName)
        {
            Clients.All.UpdateGroupList(roomName, "add");
        }

        public void DeleteRoom(string roomName)
        {
            foreach (var user in _userGroups.Keys)
            {
                List<string> roomList;
                _userGroups.TryGetValue(user, out roomList);
                if (roomList.Contains(roomName))
                {
                    roomList.Remove(roomName);
                }
            }

            Clients.All.UpdateGroupList(roomName, "remove");
        }

        public void DeliverMessage(string message)
        {
            List<string> grpList;
            var fetchResult = _userGroups.TryGetValue(Context.ConnectionId, out grpList);
            Clients.Groups(grpList.ToArray()).flushMessage(Context.ConnectionId, message);
        }
    }
}