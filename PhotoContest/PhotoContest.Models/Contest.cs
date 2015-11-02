using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Models
{
   public class Contest
   {
        private ICollection<ContestPicture> contestPictures;
       private ICollection<User> contestors;
       private ICollection<User> invitedUsers;
       private ICollection<User> committee;
       private ICollection<User> winners; 

       public Contest()
       {
           this.contestPictures = new HashSet<ContestPicture>();
            this.contestors = new HashSet<User>();
           this.CreatedOn = DateTime.Now;
           this.PrizeCount = 1;
           this.NumberOfParticipants = 0;
            this.invitedUsers = new HashSet<User>();
            this.committee = new HashSet<User>();
            this.winners = new HashSet<User>();
       }
            
       [Key]
       public int Id { get; set; }

       [Required]
       public string Title { get; set; }

       [Required]
       public string Description { get; set; }

       [Required]
       public string OwnerId { get; set; }

       public virtual User Owner { get; set; }

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
       public int PrizeValues { get; set; }

       [Required]
       public int PrizeCount { get; set; }

       public IsClosed IsClosed { get; set; }
       
        public virtual ICollection<ContestPicture> ContestPictures
        {
            get { return this.contestPictures; }
            set { this.contestPictures = value; }
        }

       public virtual ICollection<User> Contestors
       {
           get { return this.contestors; }
            set { this.contestors = value; }
       }

       public virtual ICollection<User> InvitedUsers
       {
           get { return this.invitedUsers; }
            set { this.invitedUsers = value; }
       }

       public virtual ICollection<User> Committee
       {
           get { return this.committee; }
            set { this.committee = value; }
       }

       public virtual ICollection<User> Winners
       {
           get { return this.winners; }
            set { this.winners = value; }
       } 
    }
}
