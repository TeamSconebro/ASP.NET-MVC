using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Models;

namespace PhotoContest.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotoContestContext : IdentityDbContext<IdentityUser>
    {
       
        public PhotoContestContext()
            : base("name=PhotoContestContext")
        {
            //var migrationStrategy = new MigrateDatabaseToLatestVersion<PhotoContestContext, Configuration>();
            //Database.SetInitializer(migrationStrategy);
        }


        public virtual IDbSet<User> Users { get; set; } 

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}