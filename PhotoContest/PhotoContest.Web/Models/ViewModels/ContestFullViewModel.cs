namespace PhotoContest.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class ContestFullViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string OwnerName { get; set; }

        public string VotingStrategy { get; set; }

        public string RewardStrategy { get; set; }

        public string ParticipationStrategy { get; set; }

        public string DeadlineStrategy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? NumberOfParticipants { get; set; }

        public DateTime Deadline { get; set; }

        public string PrizeValues { get; set; }

        public int PrizeCount { get; set; }

        public string IsClosed { get; set; }

        public IEnumerable<ListPictureViewModel> ContestPictures { get; set; }

        public IEnumerable<UserFullViewModel> Winners { get; set; } 
    }
}