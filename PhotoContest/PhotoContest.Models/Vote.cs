using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models
{
   public  class Vote
    {
       public Vote()
       {
           this.VotedOn = DateTime.Now;
       }

       [Key]
       public int Id { get; set; }

       public DateTime VotedOn { get; set; }
      
       [Required]
       public string UserId { get; set; }

       public virtual User User { get; set; }

       [Required]
       public int PictureId { get; set; }

       public virtual ContestPicture Picture { get; set; }
    }
}
