using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Models;
using PhotoContest.Models.Enumerations;

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
            if (!context.Users.Any())
            {
                // Seed initial data only if the database is empty
                //return;
                var users = this.SeedApplicationUsers(context);
            }

            if (context.Users.Any() && !context.Contests.Any())
            {
                this.SeedContests(context);
            }

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

        // Seed contests
        private void SeedContests(PhotoContestContext context)
        {
            var contests = new List<Contest>()
            {
                new Contest()
                {
                    Title = "Summer vacation",
                    Description = "Cool pics from summer ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(20),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.No
                },
                new Contest()
                {
                    Title = "Where were you last weekend?",
                    Description = "Photographs from weekend ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(15),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.No
                },
                new Contest()
                {
                    Title = "Chrismas atmosphere",
                    Description = "Let's look what you have ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(50),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.No
                },
                new Contest()
                {
                    Title = "Pets",
                    Description = "Lovely pets pics ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(10),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.No
                },
                new Contest()
                {
                    Title = "Technical contest",
                    Description = "Share pics of you most valuable gadget ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(20),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.No
                },
            };

            foreach (var contest in contests)
            {
                context.Contests.Add(contest);
            }

            context.SaveChanges();
        } 
    
    }
}
