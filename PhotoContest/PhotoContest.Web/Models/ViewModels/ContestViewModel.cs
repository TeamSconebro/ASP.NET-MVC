using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.Models.ViewModels
{
    public class ContestViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public string VotingStrategy { get; set; } 

        public string RewardStrategy { get; set; } 

        public string ParticipationStrategy { get; set; } 

        public string DeadlineStrategy { get; set; } 

        public string PrizeValues { get; set; }

        public int PrizeCount { get; set; }

        public string IsClosed { get; set; }
    }
}