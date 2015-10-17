using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Models
{
   public  class Contest
    {
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

       [Required]
       public string PrizeValues { get; set; }
       [Required]
       public int PrizeCount { get; set; }
       public virtual ICollection<ContestPicture> ContestPictures { get; set; }
       public string ContestorsId { get; set; }
       public virtual User Contestor { get; set; }
       public IsClosed IsClosed { get; set; }
    }
}
