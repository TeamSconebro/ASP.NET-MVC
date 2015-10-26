using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Web.Models.BindingModel;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Controllers
{
    public class NotificationsController : BaseController
    {
        public NotificationsController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Notifications
        public ActionResult Index()
        {
            // TODO: Return view model with all user notifications. Details to show: notification content, type and date.
            // TODO: Add "DateRecieved" property in Notification entity class.

            return View(new List<NotificationViewModel>());
        }

        public ActionResult CreateNotification(NotificationBindingModel model)
        {
            // TODO: Recieve binding model and create new notification in database.

            return this.View();
        }

    }
}