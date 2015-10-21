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

        public IEnumerable<ContestViewModel> ContestViewModels { get; set; }

        public IEnumerable<ContestPictureViewModel> ContestPictureViewModels { get; set; }

        public IEnumerable<NotificationViewModel> NotificationViewModels { get; set; }

    }
}