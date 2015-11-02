using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Data.Migrations;
using PhotoContest.Models;

namespace PhotoContest.Data
{
    using System.Data.Entity;

    public class PhotoContestContext : IdentityDbContext<User>
    {
       
        public PhotoContestContext()
            : base("name=PhotoContestContext")
        {

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContestContext, Configuration>());
            
        }

        public static PhotoContestContext Create()
        {
            return new PhotoContestContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Contests)
                .WithMany(c => c.Contestors)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ContestId");
                    m.ToTable("UserContests");
                });

            modelBuilder.Entity<Contest>()
                .HasRequired(c => c.Owner)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.InvitedToContests)
                .WithMany(c => c.InvitedUsers)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ContestId");
                    m.ToTable("UserContestsInvitedTo");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.InvitedToCommittees)
                .WithMany(c => c.Committee)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ContestId");
                    m.ToTable("UserCommitteeInvitedTo");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.ContestsWon)
                .WithMany(c => c.Winners)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ContestId");
                    m.ToTable("UserContestsWon");
                });


            base.OnModelCreating(modelBuilder);
        }


        public virtual IDbSet<Contest> Contests { get; set; }

        public virtual IDbSet<ContestPicture> ContestPictures { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; } 
        

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}