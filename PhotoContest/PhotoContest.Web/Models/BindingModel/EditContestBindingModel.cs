﻿namespace PhotoContest.Web.Models.BindingModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using PhotoContest.Models.Enumerations;

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