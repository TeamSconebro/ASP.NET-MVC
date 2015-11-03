using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Web.Models.BindingModel
{
    public class EditContestBindingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public VotingStrategy VotingStrategy { get; set; }

        [Required]
        public RewardStrategy RewardStrategy { get; set; }

        [Required]
        public ParticipationStrategy ParticipationStrategy { get; set; }

        [Required]
        public DeadlineStrategy DeadlineStrategy { get; set; }

        public DateTime? Deadline { get; set; }

        public int NumberOfParticipants { get; set; }

        [Required]
        public int PrizeValues { get; set; }

        [Required]
        public int PrizeCount { get; set; }
    }
}