using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoContest.Models;

namespace PhotoContest.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Coints { get; set; }
        public string PhoneNumber { get; set; }

        public IOrderedQueryable<ContestUserProfileViewModel> ContestViewModels { get; set; }

        public IQueryable<ContestPicture> ContestPictureViewModels { get; set; }

        public ICollection<Notification> NotificationViewModels { get; set; }

    }
}