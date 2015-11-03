
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public ActionResult AllActiveContests()
        {
            var activeContests = this.Data.Contests.All()
                .Where(c=>c.IsClosed == IsClosed.No)
                .OrderByDescending(c => c.CreatedOn);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModelActiveInactivePage>>(activeContests);

            return this.View(contestModels);
        }


        [ActionName("Inactive")]
        public ActionResult AllInactiveContests()
        {
            var inactiveContests = this.Data.Contests.All()
               .Where(c => c.IsClosed == IsClosed.Yes)
               .OrderByDescending(c => c.CreatedOn);

            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModelActiveInactivePage>>(inactiveContests);

            return this.View(contestModels);
            
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
            //TODO: Return view model with all detals for one particular contest. That view is rendered on Contest Details page in Visitors, Logged users and Administrators accounts.

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
           
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();
                var currentUser = this.Data.Users.Find(currentUserId);
                var contest = new Contest()
                {
                    Title = newContest.Title,
                    Description = newContest.Description,
                    CreatedOn = newContest.CreatedOn,
                    OwnerId =currentUserId,
                    ParticipationStrategy = newContest.ParticipationStrategy,
                    NumberOfParticipants = newContest.NumberOfParticipants,
                    DeadlineStrategy = newContest.DeadlineStrategy,
                    Deadline = newContest.Deadline,
                    PrizeCount = newContest.PrizeCount,
                    PrizeValues = newContest.PrizeValues*2,
                    RewardStrategy = newContest.RewardStrategy,
                    VotingStrategy = newContest.VotingStrategy
                };

                this.Data.Contests.Add(contest);
                currentUser.Contests.Add(contest);
                this.Data.Contests.SaveChanges();

                this.TempData["message-create-contest-success"] = "You successfully created new contest!";

                return RedirectToAction("ContestDetails", "Contests", new {id = contest.Id});
            }
            

            return this.View(newContest);
        }

        public ActionResult EditContest()
        {
            // TODO: Recieve binding model and modify existing contest.

            return this.View();
        }

        public ActionResult DeleteContest()
        {
            // TODO: Delete contest by id.

            return this.View();
        }

        public ActionResult AddToContest(string username, int contestId)
        {
            var resultMessage = "";

            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                resultMessage = "No such user!";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            var contest = this.Data.Contests.Find(contestId);
            if (contest == null)
            {
                resultMessage = "No such contest!";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            contest.InvitedUsers.Add(user);
            user.InvitedToContests.Add(contest);
            this.Data.SaveChanges();

            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

        /**********************************************/

        public ActionResult CreateImageBindingModel(ImageBindingModelUserInput partialModel, int id)
        {
            //int contestId = RouteData.Values["id"];

            var imageBindingModel = new ImageBindingModel()
            {
                
            };

            return RedirectToAction("UploadImage", "Pictures", imageBindingModel);
        }
    }
}