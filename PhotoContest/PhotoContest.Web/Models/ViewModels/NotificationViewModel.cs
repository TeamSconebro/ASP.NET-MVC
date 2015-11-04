using System;

namespace PhotoContest.Web.Models.ViewModels
{
    public class NotificationViewModel
    {
        public string NotificationContent { get; set; }

        public string NotificationType { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}