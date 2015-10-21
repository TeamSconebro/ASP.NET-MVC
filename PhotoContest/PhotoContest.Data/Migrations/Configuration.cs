using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Models;

namespace PhotoContest.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<PhotoContest.Data.PhotoContestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhotoContest.Data.PhotoContestContext context)
        {
            if (context.Users.Any())
            {
                // Seed initial data only if the database is empty
                return;
            }

            var users = this.SeedApplicationUsers(context);
        }

        private IList<User> SeedApplicationUsers(
            PhotoContestContext context)
        {
            var usernames = new string[] { "joro", "tanya", "bobi" };

            var users = new List<User>();
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 2,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            foreach (var username in usernames)
            {
                var user = new User
                {
                    UserName = username,
                    Email = username + "@gmail.com"
                };

                var password = username;
                var userCreateResult = userManager.Create(user, password);
                if (userCreateResult.Succeeded)
                {
                    users.Add(user);
                }
                else
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }
            }

            // Create "Administrator" role
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            // Add "admin" user to "Administrator" role
            var adminUser = users.First(user => user.UserName == "joro");
            var adminUser1 = users.First(user => user.UserName == "tanya");
            var adminUser2 = users.First(user => user.UserName == "bobi");
            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, "Administrator");
            var addAdminRoleResult1 = userManager.AddToRole(adminUser1.Id, "Administrator");
            var addAdminRoleResult2 = userManager.AddToRole(adminUser2.Id, "Administrator");
            if (!addAdminRoleResult.Succeeded && !addAdminRoleResult1.Succeeded && !addAdminRoleResult2.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }

            // add "User" role
            var roleCreateUser = roleManager.Create(new IdentityRole("User"));
            if (!roleCreateUser.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateUser.Errors));
            }

            context.SaveChanges();

            return users;
        }
    
    }
}
