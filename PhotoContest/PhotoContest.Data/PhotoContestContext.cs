using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Data.Migrations;
using PhotoContest.Models;

namespace PhotoContest.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotoContestContext : IdentityDbContext<User>
    {
       
        public PhotoContestContext()
            : base("name=PhotoContestContext")
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContestContext, PhotoContest.Data.Migrations.Configuration>());
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