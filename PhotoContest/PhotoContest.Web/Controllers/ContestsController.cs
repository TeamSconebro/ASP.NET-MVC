
using System;
using System.ComponentModel.Design.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using PhotoContest.Web.Helpers;

namespace PhotoContest.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitsOfWork;
    using PhotoContest.Models;
    using PhotoContest.Models.Enumerations;
    using Models.BindingModel;
    using Models.ViewModels;
    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) : base(data)
        {
        }
        
        public ActionResult GetContest(int id)
        {
            var currentContest = this.Data.Contests.Find(id);
            if (currentContest == null)
            {
                return this.HttpNotFound();
            }

            var contestModel = Mapper.Map<Contest, ContestFullViewModel>(currentContest);

            return this.View(contestModel);
        }
        
        [ActionName("Active")]
        public ActionResult AllActiveContests(int? page)
        {
            var activeContests = this.Data.Contests.All()
                .Where(c=>c.IsClosed == IsClosed.No)
                .OrderByDescending(c => c.CreatedOn);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModelActiveInactivePage>>(activeContests);

            var pageNumber = page ?? 1;
            return this.View(contestModels.ToPagedList(pageNumber, Paging.ItemsPerPage));
        }


        [ActionName("Inactive")]
        public ActionResult AllInactiveContests(int? page)
        {
            var inactiveContests = this.Data.Contests.All()
               .Where(c => c.IsClosed == IsClosed.Yes)
               .OrderByDescending(c => c.CreatedOn);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModelActiveInactivePage>>(inactiveContests);

            var pageNumber = page ?? 1;
            return this.View(contestModels.ToPagedList(pageNumber, Paging.ItemsPerPage));
        }

        // List all active/inactive contests with more details (after pressing "See more" button)
        public ActionResult AllContests()
        {
            var allContests = this.Data.Contests
              .All()
              .OrderByDescending(c => c.CreatedOn);
            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(allContests);
            return this.View(contestModels);
        }

        [HttpGet]
        public ActionResult ContestDetails(long id=98567457552123554)
        {
            if (id == 98567457552123554)
            {
                id = this.Data.Contests.All().OrderByDescending(c => c.CreatedOn).Select(c=>c.Id).FirstOrDefault();
            }
            var contestDetails = this.Data.Contests.Find(id);
            var contestModel = Mapper.Map<Contest,ContestFullViewModel>(contestDetails);

            return this.View(contestModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateContest()
        {
            
            return this.View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContest(CreateContestBindingModel newContest)
        {
            if (newContest.DeadlineStrategy == DeadlineStrategy.ByTime)
            {
                if (newContest.Deadline > DateTime.Now)
                {
                    if (ModelState.IsValid)
                    {

                        var currentUserId = User.Identity.GetUserId();
                        var currentUser = this.Data.Users.Find(currentUserId);
                        var contest = new Contest()
                        {
                            Title = newContest.Title,
                            Description = newContest.Description,
                            CreatedOn = newContest.CreatedOn,
                            OwnerId = currentUserId,
                            ParticipationStrategy = newContest.ParticipationStrategy,
                            NumberOfParticipants = newContest.NumberOfParticipants,
                            DeadlineStrategy = newContest.DeadlineStrategy,
                            Deadline = newContest.Deadline,
                            PrizeCount = newContest.PrizeCount,
                            PrizeValues = newContest.PrizeValues*2,
                            RewardStrategy = newContest.RewardStrategy,
                            VotingStrategy = newContest.VotingStrategy
                        };
                        currentUser.Coints = currentUser.Coints - contest.PrizeValues/2;
                        if (currentUser.Coints < 0)
                        {
                            this.TempData["message-create-contest-coints"] = "You do NOT have enough Coints!";
                        }
                        else
                        {
                            this.Data.Contests.Add(contest);
                            currentUser.Contests.Add(contest);
                            this.Data.Contests.SaveChanges();
                            this.TempData["message-create-contest-success"] = "You successfully created new contest!";
                            return RedirectToAction("ContestDetails", "Contests", new {id = contest.Id});
                        }
                    }
                }
            }
            else if(newContest.NumberOfParticipants>0)
            {
                 var currentUserId = User.Identity.GetUserId();
                        var currentUser = this.Data.Users.Find(currentUserId);
                        var contest = new Contest()
                        {
                            Title = newContest.Title,
                            Description = newContest.Description,
                            CreatedOn = newContest.CreatedOn,
                            OwnerId = currentUserId,
                            ParticipationStrategy = newContest.ParticipationStrategy,
                            NumberOfParticipants = newContest.NumberOfParticipants,
                            DeadlineStrategy = newContest.DeadlineStrategy,
                            Deadline = newContest.Deadline,
                            PrizeCount = newContest.PrizeCount,
                            PrizeValues = newContest.PrizeValues*2,
                            RewardStrategy = newContest.RewardStrategy,
                            VotingStrategy = newContest.VotingStrategy
                        };
                        currentUser.Coints = currentUser.Coints - contest.PrizeValues/2;
                if (currentUser.Coints < 0)
                {
                    this.TempData["message-create-contest-coints"] = "You do NOT have enough Coints!";
                }
                else
                {
                    this.Data.Contests.Add(contest);
                    currentUser.Contests.Add(contest);
                    this.Data.Contests.SaveChanges();
                    this.TempData["message-create-contest-success"] = "You successfully created new contest!";
                    return RedirectToAction("ContestDetails", "Contests", new {id = contest.Id});
                }
            }

            return this.View(newContest);
        }

        // GET: Edit contest
        [Authorize]
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditContest(int id)
        {
            var contest = this.Data.Contests.Find(id);
            if (contest == null)
            {
                return RedirectToAction("Profile", "Users");
            }

            var contestBindingModel = Mapper.Map<Contest, EditContestBindingModel>(contest);

            return View(contestBindingModel);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditContest(EditContestBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();
                var currentUser = this.Data.Users.Find(currentUserId);
                var contest = this.Data.Contests.Find(model.Id);
                if (contest == null)
                {
                    return this.RedirectToAction("Profile", "Users");
                }

                contest.Title = model.Title;
                contest.Description = model.Description;
                contest.ParticipationStrategy = model.ParticipationStrategy;
                contest.NumberOfParticipants = model.NumberOfParticipants;
                contest.DeadlineStrategy = model.DeadlineStrategy;
                contest.Deadline = model.Deadline;
                contest.PrizeCount = model.PrizeCount;
                contest.PrizeValues = model.PrizeValues*2;
                contest.RewardStrategy = model.RewardStrategy;
                contest.VotingStrategy = model.VotingStrategy;

                this.Data.SaveChanges();

                this.TempData["message-edit-contest-success"] = "You successfully edited contest!";

                return RedirectToAction("ContestDetails", "Contests", new { id = contest.Id });
            }
            
            return RedirectToAction("Profile", "Users");
        }

        // GET: Delete contest
        [Authorize]
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteContest()
        {
            return View();
        }

        // POST: Delete contest
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContest(int id)
        {
            var contest = this.Data.Contests.Find(id);
            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);
            if (contest == null)
            {
                this.TempData["message-delete-contest-error"] = "Non existing contest!";
                return RedirectToAction("Profile", "Users");
            }

            var contestTitle = contest.Title;
            this.Data.Contests.Remove(contest);
            user.Contests.Remove(contest);
            this.Data.SaveChanges();

            this.TempData["message-delete-contest-success"] = "You successfully deleted \"" + contestTitle + "\" contest!";
            return RedirectToAction("Profile", "Users");
        }

        public ActionResult NoWinner(int contestId)
        {
            var contest = this.Data.Contests.Find(contestId);
            contest.IsClosed=IsClosed.Yes;
            this.Data.SaveChanges();
            const string resultMessage = "No winner was selected";
            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WinnerByVotes(string username, int contestId)
        {
            var resultMessage = "";
            var contest = this.Data.Contests.Find(contestId);
            var numberOfWinners = contest.PrizeCount;
            contest.IsClosed = IsClosed.Yes;
            var pictureWinner = contest.ContestPictures.OrderBy(cp => cp.Votes).Take(numberOfWinners);
            if (contest.RewardStrategy == RewardStrategy.SingleWinner)
            {
                if (contest.ContestPictures.Any())
                {
                    var pictures = contest.ContestPictures.OrderBy(cp => cp.Votes.Count).FirstOrDefault();
                    if (pictures != null)
                    {
                        var firstWinnerId = pictures.OwnerId;
                        if (firstWinnerId != null)
                        {

                            var firstWinnerUser = this.Data.Users.Find(firstWinnerId);
                            firstWinnerUser.Coints = firstWinnerUser.Coints + contest.PrizeValues;
                            resultMessage = firstWinnerUser.UserName + " is the winner of " + contest.Title + "!";
                            contest.Winners.Add(firstWinnerUser);
                            firstWinnerUser.ContestsWon.Add(contest);
                        }
                    }
                } 
            }
            else
            {
                var firstOrDefault = contest.ContestPictures.OrderBy(cp => cp.Votes).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var firstWinnerId = firstOrDefault.OwnerId;
                    var firstWinnerUser = this.Data.Users.Find(firstWinnerId);
                    firstWinnerUser.Coints = firstWinnerUser.Coints + contest.PrizeValues;
                    resultMessage = firstWinnerUser.UserName + " is the winner of " + contest.Title + "!";
                }
                
                foreach (var people in pictureWinner)
                {
                    var winner = this.Data.Users.Find(people.OwnerId);
                    contest.Winners.Add(winner);
                    winner.ContestsWon.Add(contest);

                }
            }
            this.Data.SaveChanges();
            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChooseWinner(string username, int contestId)
        {
            var resultMessage = "";
            var contest = this.Data.Contests.Find(contestId);
            var userId = this.Data.Users.All().FirstOrDefault(u => u.UserName == username).Id;
            var users = this.Data.Users.Find(userId);
               
                contest.Winners.Add(users);
                if (users != null) users.ContestsWon.Add(contest);
                if (users != null) users.Coints = users.Coints + contest.PrizeValues;
                contest.IsClosed = IsClosed.Yes;
                this.Data.SaveChanges();
            
            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddToCommittee(string username, int contestId)
        {
            var resultMessage = "";

            // Check whether invited user exist
            var invitedToCommitteeUser = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (invitedToCommitteeUser == null)
            {
                resultMessage = UserMessage.NoUser;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether author and invited to committee user are same person
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.Find(currentUserId);
            if (currentUser.UserName == invitedToCommitteeUser.UserName)
            {
                resultMessage = UserMessage.CannotInviteYourself;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether contest exist
            var contest = this.Data.Contests.Find(contestId);
            if (contest == null)
            {
                resultMessage = UserMessage.NoContest;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether ivited to committee user is already in the committee
            var invitedUsersNames = contest.InvitedUsers.Select(iu => iu.UserName).ToList();
            if (invitedUsersNames.Contains(invitedToCommitteeUser.UserName))
            {
                resultMessage = UserMessage.UserIsInTheCommittee;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            contest.Committee.Add(invitedToCommitteeUser);
            invitedToCommitteeUser.InvitedToCommittees.Add(contest);
            this.Data.SaveChanges();

            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddToContest(string username, int contestId)
        {
            var resultMessage = "";

            // Check whether invited user exist
            var invitedUser = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (invitedUser == null)
            {
                resultMessage = UserMessage.NoUser;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether author and invited user are same person
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.Find(currentUserId);
            if (currentUser.UserName == invitedUser.UserName)
            {
                resultMessage = UserMessage.CannotInviteYourself;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether contest exist
            var contest = this.Data.Contests.Find(contestId);
            if (contest == null)
            {
                resultMessage = UserMessage.NoContest;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether ivited user is already in the contest
            var invitedUsersNames = contest.InvitedUsers.Select(iu => iu.UserName).ToList();
            if (invitedUsersNames.Contains(invitedUser.UserName))
            {
                resultMessage = UserMessage.UserIsInTheContest;
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            contest.InvitedUsers.Add(invitedUser);
            invitedUser.InvitedToContests.Add(contest);
            this.Data.SaveChanges();

            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }
    }
}