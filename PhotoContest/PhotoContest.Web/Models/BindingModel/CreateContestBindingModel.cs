

using System.ComponentModel;

namespace PhotoContest.Web.Models.BindingModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using PhotoContest.Models;
    using PhotoContest.Models.Enumerations;
    public class CreateContestBindingModel
    {

       public CreateContestBindingModel()
       {
           this.CreatedOn = DateTime.Now;
       }
        [Key]
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }
        [Required]
        public VotingStrategy VotingStrategy { get; set; }

        [Required]
        public RewardStrategy RewardStrategy { get; set; }

        [Required]
        public ParticipationStrategy ParticipationStrategy { get; set; }

        [Required]
        public DeadlineStrategy DeadlineStrategy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? Deadline { get; set; }
        public int NumberOfParticipants { get; set; }
        [Required]
        public string PrizeValues { get; set; }

        [Required]
        public int PrizeCount { get; set; }
    }
}