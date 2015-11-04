

namespace PhotoContest.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Coints { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<ContestUserProfileViewModel> ContestViewModels { get; set; }

        public IEnumerable<ContestPictureUserProfileViewModel> ContestPictureViewModels { get; set; }
    }
}