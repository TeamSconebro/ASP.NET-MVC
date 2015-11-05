using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PhotoContest.Models;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Hubs
{
    public class NotificationsHub : Hub
    {
        public override Task OnConnected()
        {
            string id = this.Context.User.Identity.GetUserId();
            this.Groups.Add(this.Context.ConnectionId, id);

            return base.OnConnected();
        }

        public void SendNotification(string userId)
        {
            var testId = Context.ConnectionId;
            this.Clients.User(userId).receiveNotification();
        }
    }
}