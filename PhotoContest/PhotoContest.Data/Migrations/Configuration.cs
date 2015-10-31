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
                var users = this.SeedApplicationUsers(context);
            }

            if (context.Users.Any() && !context.Contests.Any())
            {
                this.SeedContests(context);
            }

            if (context.Users.Any() && context.Contests.Any() && !context.ContestPictures.Any())
            {
                this.SeedContestPictures(context);
            }

        }

        // Seed users
        private IList<User> SeedApplicationUsers(PhotoContestContext context)
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
                    IsClosed = IsClosed.No,
                    CreatedOn = DateTime.Now
                },
                new Contest()
                {
                    Title = "Summer vacation CLOSED",
                    Description = "Cool pics from summer ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-1),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-12)
                },
                new Contest()
                {
                    Title = "Summer vacation 2",
                    Description = "Cool pics from summer 2 ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-3),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-17)
                },
                new Contest()
                {
                    Title = "Summer vacation 3",
                    Description = "Cool pics from summer 3...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-5),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-25)
                },
                new Contest()
                {
                    Title = "Summer vacation 4",
                    Description = "Cool pics from summer ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-2),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-15)
                },
                new Contest()
                {
                    Title = "Summer vacation 5",
                    Description = "Cool pics from summer ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-8),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-18)
                },
                new Contest()
                {
                    Title = "Summer vacation 6",
                    Description = "Cool pics from summer ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    VotingStrategy = VotingStrategy.Closed,
                    RewardStrategy = RewardStrategy.TopNPrizes,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-1),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-50)
                },
                new Contest()
                {
                    Title = "Summer vacation 7",
                    Description = "Cool pics from summer 7 ...",
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    VotingStrategy = VotingStrategy.Open,
                    RewardStrategy = RewardStrategy.SingleWinner,
                    ParticipationStrategy = ParticipationStrategy.Open,
                    DeadlineStrategy = DeadlineStrategy.ByTime,
                    Deadline = DateTime.Now.AddDays(-5),
                    NumberOfParticipants = 0,
                    PrizeValues = "100",
                    PrizeCount = 1,
                    IsClosed = IsClosed.Yes,
                    CreatedOn = DateTime.Now.AddDays(-30)
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
                    IsClosed = IsClosed.No,
                    CreatedOn = DateTime.Now
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
                    IsClosed = IsClosed.No,
                    CreatedOn = DateTime.Now
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
                    IsClosed = IsClosed.No,
                    CreatedOn = DateTime.Now
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
                    IsClosed = IsClosed.No,
                    CreatedOn = DateTime.Now
                },
            };

            foreach (var contest in contests)
            {
                context.Contests.Add(contest);
            }

            context.SaveChanges();
        }

        // Seed contest pictures
        private void SeedContestPictures(PhotoContestContext context)
        {
            var contestPictures = new List<ContestPicture>()
            {
                new ContestPicture()
                {
                    Title = "Title 1",
                    Base64Data = "http://i.telegraph.co.uk/multimedia/archive/03235/potd-husky_3235255k.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 2",
                    Base64Data = "http://7-themes.com/data_images/out/24/6850590-baby-fox-pictures.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Pets")
                },
                new ContestPicture()
                {
                    Title = "Title 22",
                    Base64Data = "http://petrecognition.com/wp-content/uploads/2013/08/PetProtection.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Pets")
                },
                new ContestPicture()
                {
                    Title = "Title 222",
                    Base64Data = "http://livebooklet.com/userFiles/a/2/2/8/7/9/9/EIQaubIqq1dPk8jfrfNRM0/jealLvMz.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Pets")
                },
                new ContestPicture()
                {
                    Title = "Title 82",
                    Base64Data = "http://www.personal.psu.edu/afr3/blogs/siowfa13/Tigers-animals-20238015-2493-1983.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Pets")
                },
                new ContestPicture()
                {
                    Title = "Title 3",
                    Base64Data = "http://www.snappypixels.com/wp-content/uploads/2013/08/bunch-of-random-funny-pictures-6.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Chrismas atmosphere")
                },
                new ContestPicture()
                {
                    Title = "Title 4",
                    Base64Data = "http://coolwildlife.com/wp-content/uploads/galleries/post-345/Wolf%20Pictures%20034.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Chrismas atmosphere")
                },
                new ContestPicture()
                {
                    Title = "Title 444",
                    Base64Data = "http://cdn-media-2.lifehack.org/wp-content/files/2014/12/xmass-tree.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Chrismas atmosphere")
                },
                new ContestPicture()
                {
                    Title = "Title 784",
                    Base64Data = "http://www.2ch.com/sites/www.2ch.com/files/field/image/201412/colorful-christmas-tree.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Chrismas atmosphere")
                },
                new ContestPicture()
                {
                    Title = "Title 5",
                    Base64Data = "http://static.independent.co.uk/s3fs-public/styles/story_large/public/thumbnails/image/2013/01/24/12/v2-cute-cat-picture.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 6",
                    Base64Data = "http://www.planwallpaper.com/static/images/cool_picture.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 7",
                    Base64Data = "http://7-themes.com/data_images/out/73/7020359-best-autumn-pictures.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 8",
                    Base64Data = "https://s-media-cache-ak0.pinimg.com/236x/4f/2d/a0/4f2da0f1e6e3ee767214407c6a5aee04.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 9",
                    Base64Data = "http://www.airshows.co.uk/week-in-pictures/2013/may/images/week-in-pictures-08.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 10",
                    Base64Data = "https://pbs.twimg.com/profile_images/425946167050911744/x62a9eBz_400x400.jpeg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title 11",
                    Base64Data = "http://ichef.bbci.co.uk/news/976/media/images/83351000/jpg/_83351965_explorer273lincolnshirewoldssouthpicturebynicholassilkstone.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Technical contest")
                },
                new ContestPicture()
                {
                    Title = "Title summer",
                    Base64Data = "http://www.bbb.org/blog/wp-content/uploads/2011/05/summer2.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation")
                },
                new ContestPicture()
                {
                    Title = "Title 123",
                    Base64Data = "http://www.alfaplam.rs/upload/News/Image/2015_06/frame_01.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation")
                },
                new ContestPicture()
                {
                    Title = "Title 159",
                    Base64Data = "https://d1ciw9phtlkz3p.cloudfront.net/trip-ideas/Summer/Header-Image-Small.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 2")
                },
                new ContestPicture()
                {
                    Title = "Title 147",
                    Base64Data = "http://thenewblack.gr/wp-content/uploads/2015/07/tips-for-inexpensive-summer-vacation.png",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 2")
                },
                new ContestPicture()
                {
                    Title = "Title 159",
                    Base64Data = "http://activerain.com/image_store/uploads/agents/hatteam/files/summer-vacation.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 3")
                },
                new ContestPicture()
                {
                    Title = "Title 147",
                    Base64Data = "https://mayrsom.files.wordpress.com/2015/06/summer-vacation-beach-734.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 3")
                },
                new ContestPicture()
                {
                    Title = "Title 155",
                    Base64Data = "http://www.ritzgroup.org/wp-content/uploads/2014/06/Hawaii-For-Summer-Vacation-1728x1080.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 4")
                },
                new ContestPicture()
                {
                    Title = "Title 146",
                    Base64Data = "http://mcselect.ru/wp-content/uploads/2013/07/v22-1024x768.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 4")
                },
                new ContestPicture()
                {
                    Title = "Title 151",
                    Base64Data = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcS0y-0PxKXY8Ist5mA0OoP0TYr2_1Qz4TZk-0WodwFE4RLETPIK",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 5")
                },
                new ContestPicture()
                {
                    Title = "Title 149",
                    Base64Data = "https://s-media-cache-ak0.pinimg.com/736x/00/79/75/0079756323cb0afe749bc04fb6846055.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 5")
                },
                new ContestPicture()
                {
                    Title = "Title 168",
                    Base64Data = "http://media-cdn.tripadvisor.com/media/photo-o/07/25/a4/3e/sonora-resort.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 6")
                },
                new ContestPicture()
                {
                    Title = "Title 73",
                    Base64Data = "http://www.bestourism.com/img/items/big/725/Maui-in-Hawaii_Road-to-Hana_2867.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 6")
                },
                new ContestPicture()
                {
                    Title = "Title 168",
                    Base64Data = "http://cf.ltkcdn.net/travel/images/std/187173-425x283-Waimea-Bay-Oahu-Hawaii.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 7")
                },
                new ContestPicture()
                {
                    Title = "Title 73",
                    Base64Data = "http://images.freehdw.com/800/nature-landscapes_widewallpaper_summer-vacation_4572.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation 7")
                },
                new ContestPicture()
                {
                    Title = "Title 412",
                    Base64Data = "http://files.cityweekend.com.cn/storage/article/images/sliders/weekend_0_0.png",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "tanya"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Where were you last weekend?")
                },
                new ContestPicture()
                {
                    Title = "Title 737",
                    Base64Data = "http://lifengrguardiannewscom.c.presscdn.com/wp-content/uploads/2015/08/weekend-lifemag.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Where were you last weekend?")
                },
                new ContestPicture()
                {
                    Title = "Title 257",
                    Base64Data = "http://previews.123rf.com/images/smithytomy/smithytomy1309/smithytomy130900014/21943168-Summer-vacation-greeting-card-Vector-illustration-Stock-Vector-beach.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "joro"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation CLOSED")
                },
                new ContestPicture()
                {
                    Title = "Title 391",
                    Base64Data = "http://static.travel.usnews.com/images/articles/656/MAIN.jpg",
                    //VotesCount = 0,
                    Owner = context.Users.FirstOrDefault(u => u.UserName == "bobi"),
                    Contest = context.Contests.FirstOrDefault(c => c.Title == "Summer vacation CLOSED")
                },
                
            };

            foreach (var contestPicture in contestPictures)
            {
                context.ContestPictures.Add(contestPicture);
            }

            context.SaveChanges();
        }

    }
}
