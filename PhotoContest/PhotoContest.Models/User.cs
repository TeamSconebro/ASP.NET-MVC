using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoContest.Models
{
    public class User: IdentityUser
    {
        private ICollection<Vote> votes;
        private ICollection<Contest> contests;
        private ICollection<Notification> notifications;
        private ICollection<ContestPicture> contestPictures;

        public User()
        {
            this.votes=new HashSet<Vote>();
            this.contests=new HashSet<Contest>();
            this.notifications=new HashSet<Notification>();
            this.contestPictures=new HashSet<ContestPicture>();
        }
        [MinLength(4)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(4)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string Base64Data { get; set; }

        public virtual ICollection<Contest> Contests { get; set; }
        //public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<ContestPicture> ContestPictures { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
