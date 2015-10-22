using System;
using System.Linq;
using System.Web.Routing;
using PhotoContest.Models;

namespace PhotoContest.Web.Controllers
{
    using System.Web.Mvc;
    using Data.UnitsOfWork;
    public class BaseController : Controller
    {
        public BaseController(IPhotoContestData data)
        {
            this.Data = data;
        }

        public BaseController(IPhotoContestData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        protected IPhotoContestData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}