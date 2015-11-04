namespace PhotoContest.Web.Models.ViewModels
{
    using System.Collections.Generic;
    using PhotoContest.Models;
    using PhotoContest.Web.Models.BindingModel;

    public class UserFullViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageBase64Data { get; set; }

        public string ImageUrl { get; set; }

        public int Coints { get; set; }

        public IEnumerable<ContestViewModel> Contests;

        public IEnumerable<VoteBindingModel> Votes;

        public IEnumerable<Notification> Notifications;

        public IEnumerable<ContestPictureViewModel> ContestPictures;

        public IEnumerable<ContestFullViewModel> InvitedToContests;

        public IEnumerable<ContestFullViewModel> InvitedToCommittees;

        public IEnumerable<ContestFullViewModel> ContestsWon; 
 
    }
}