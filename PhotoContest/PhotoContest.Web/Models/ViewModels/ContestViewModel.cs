using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using PhotoContest.Models;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Web.Models.ViewModels
{
    public class ContestViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public User Owner { get; set; }

        public VotingStrategy VotingStrategy { get; set; } 

        public RewardStrategy RewardStrategy { get; set; } 

        public ParticipationStrategy ParticipationStrategy { get; set; } 

        public DeadlineStrategy DeadlineStrategy { get; set; }
        public int? NumberOfParticipants { get; set; }
        public DateTime Deadline { get; set; }

        public string PrizeValues { get; set; }

        public int PrizeCount { get; set; }

        public string IsClosed { get; set; }
    }
}