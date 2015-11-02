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
        private ICollection<Contest> invitedToContests;
        private ICollection<Contest> invitedToCommittees;
        private ICollection<Contest> contestsWon;


        public User()
        {
            this.votes=new HashSet<Vote>();
            this.contests=new HashSet<Contest>();
            this.notifications=new HashSet<Notification>();
            this.contestPictures=new HashSet<ContestPicture>();
            this.Coints = 1000;
            this.invitedToContests = new HashSet<Contest>();
            this.invitedToCommittees = new HashSet<Contest>();
            this.contestsWon = new HashSet<Contest>();
        }

        //[Required]
        //[MinLength(4)]
        //[MaxLength(20)]
        //[RegularExpression(@"([0-9a-zA-Z\\_\\-]+)")]
        //public string Username { get; set; }

        //[MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        //[MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string ImageBase64Data { get; set; }
        public string ImageUrl { get; set; }
        public int Coints { get; set; }

        public virtual ICollection<Contest> Contests
        {
            get { return this.contests; }
            set { this.contests = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public virtual ICollection<Notification> Notifications
        {
            get { return this.notifications; }
            set { this.notifications = value; }
        }

        public virtual ICollection<ContestPicture> ContestPictures
        {
            get { return this.contestPictures; }
            set { this.contestPictures = value; }
        }

        public virtual ICollection<Contest> InvitedToContests
        {
            get { return this.invitedToContests; }
            set { this.invitedToContests = value; }
        }
  
        public virtual ICollection<Contest> InvitedToCommittees
        {
            get { return this.invitedToCommittees; }
            set { this.invitedToCommittees = value; }
        }

        public virtual ICollection<Contest> ContestsWon
        {
            get { return this.contestsWon; }
            set { this.contestsWon = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
