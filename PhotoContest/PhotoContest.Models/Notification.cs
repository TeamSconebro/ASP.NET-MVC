using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Models.Enumerations;

namespace PhotoContest.Models
{
   public  class Notification
    {
       [Key]
       public int Id { get; set; }

       [Required]
       public string NotificationContent { get; set; }

       [Required]
       public NotificationType Type { get; set; }

       [Required]
       public string UserId { get; set; }

       public virtual  User User { get; set; }
    }
}
