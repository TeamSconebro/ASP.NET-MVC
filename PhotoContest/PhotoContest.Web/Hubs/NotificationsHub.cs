﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PhotoContest.Models;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Hubs
{
    //[HubName("notifications")]
    public class NotificationsHub : Hub
    {
        //public void SendNotification(Notification notification)
        //{
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
        //    hubContext.Clients.Client(notification.UserId).recieveNotification(notification);
        //}

        public void SendNotifications(string message)
        {
            Clients.All.receiveNotification(message);
        }
    }
}