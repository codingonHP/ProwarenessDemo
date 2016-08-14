using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ProawarenessMeetupDemos.Database;
using ProawarenessMeetupDemos.Hubs;

namespace ProawarenessMeetupDemos.Utils
{
    public class ClientManager
    {
       
        internal void LogOutFromAllClients(string userName)
        {
            using (var context = new ProwarenessContext())
            {
                var userId = context.AspNetUsers.First(u => u.UserName == userName).Id;
                var allLoggedInClients = context.ActiveSessions.Where(a => a.UserId == userId);
                var allLoggedInClientsIds = allLoggedInClients.Select(x => x.SessionID).ToList();

                context.ActiveSessions.RemoveRange(allLoggedInClients);
                var userLoginHubContext = GlobalHost.ConnectionManager.GetHubContext<UserLoginHub>();
                userLoginHubContext.Clients.Clients(allLoggedInClientsIds).LogOut();

                context.SaveChanges();
            }
        }

        internal void AddToActiveLoggedInClientList(string userId, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(connectionId))
            {
                return;
            }

            using (var context = new ProwarenessContext())
            {
                context.ActiveSessions.Add(new ActiveSession
                {
                    ConnectedAt = DateTime.Now,
                    UserId = context.AspNetUsers.FirstOrDefault(u => u.UserName == userId).Id,
                    SessionID = connectionId
                });

                context.SaveChanges();
            }
        }

        internal void RemoveUserFromClientsList(string userId, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(connectionId))
            {
                return;
            }

            using (var context = new ProwarenessContext())
            {
                var client = context.ActiveSessions.Where(s => s.SessionID == connectionId);
                if (client != null)
                {
                    context.ActiveSessions.RemoveRange(client);
                    context.SaveChanges();

                }
            }

        }

    }
}