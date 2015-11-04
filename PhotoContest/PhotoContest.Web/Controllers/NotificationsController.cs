using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Models;
using PhotoContest.Web.Helpers;
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
        [Authorize]
        public ActionResult Index(int? page)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.Find(currentUserId);
            if (currentUser == null)
            {
                return this.HttpNotFound();
            }

            var userNotifications = currentUser.Notifications.OrderByDescending(n => n.CreatedOn);
            var usernotificationModel = Mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationViewModel>>(userNotifications);

            var pageNumber = page ?? 1;
            return this.View(usernotificationModel.ToPagedList(pageNumber, Paging.ItemsPerPage));
        }

        public ActionResult CreateNotification(NotificationBindingModel model)
        {
            // TODO: Recieve binding model and create new notification in database.

            return this.View();
        }

    }
}