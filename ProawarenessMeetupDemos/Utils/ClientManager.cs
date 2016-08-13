using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ProawarenessMeetupDemos.Database;

namespace ProawarenessMeetupDemos.Utils
{
    public class ClientManager
    {
        internal bool IsSessionActive(string userName, out string userId)
        {
            using (var context = new ProwarenessContext())
            {
                var user = context.AspNetUsers.FirstOrDefault(u => u.UserName.Equals(userName));
                if (user != null)
                {
                    userId = user.Id;
                    return user.ActiveSessions?.Any() ?? false;
                }

                throw new KeyNotFoundException($"user not found : {userName}");
            }
        }

        internal void AddToActiveLoggedInClientList(string userId, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(connectionId))
            {
                throw new ArgumentNullException("userId or connectionId is null or empty");
            }

            using (var context = new ProwarenessContext())
            {
                context.ActiveSessions.Add(new ActiveSession
                {
                    ConnectedAt = DateTime.Now,
                    UserId = userId,
                    SessionID = new Guid(connectionId)
                });

                context.SaveChanges();
            }
        }

    }
}