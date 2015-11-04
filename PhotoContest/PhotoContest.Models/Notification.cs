using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Models
{
   public  class Notification
    {
       public Notification()
       {
           this.CreatedOn = DateTime.Now;
       }

       [Key]
       public int Id { get; set; }

       [Required]
       public string NotificationContent { get; set; }

       [Required]
       public NotificationType Type { get; set; }

       [Required]
       public DateTime CreatedOn { get; set; }

       [Required]
       public string UserId { get; set; }

       public virtual  User User { get; set; }
    }
}
