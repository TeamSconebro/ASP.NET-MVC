using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Web.Models.BindingModel
{
    public class NotificationBindingModel
    {
        [Required]
        public string NotificationContent { get; set; }

        public NotificationType Type { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}