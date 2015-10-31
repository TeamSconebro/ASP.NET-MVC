using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Models;
using PhotoContest.Models.Enumerations;
using PhotoContest.Web.Models.BindingModel;

namespace PhotoContest.Web.Controllers
{
    public class PicturesController : BaseController
    {
        public PicturesController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Pictures
        public ActionResult AllContestPictures()
        {
            //TODO: Return view model with all pictures in one particular contest. Model should contain picture title, content (image) and author. That model should be rendered on the Contest details page.

            return View();
        }

        [HttpGet]
        public ActionResult UploadForm()
        {
            return this.PartialView("UploadForm");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(ImageBindingModel model)
        {
            var ownerId = this.User.Identity.GetUserId();
            var owner = this.Data.Users.Find(ownerId);
            if (owner == null)
            {
                return HttpNotFound();
            }
            
            return this.View();
        }

        [Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Vote(int id)
        {
            var picture = this.Data.ContestPictures.Find(id);
            var resultMessage = "";
            if (picture == null)
            {
                resultMessage = "No such picture in contest!";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // TODO: Check if current user is logged, is author of the contests, if he can vote and is he votted yet.

            // Check whether user is author of the contest
            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);
            var userContests = user.Contests.ToList();
            var currentContest = picture.Contest;
            if (currentContest.OwnerId == userId/*userContests.Contains(currentContest)*/)
            {
                resultMessage = "You cannot vote in your contest!";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether user has uploaded picture he wants to vote for
            if (picture.OwnerId == userId)
            {
                resultMessage = "You cannot vote for picture uploaded by you!";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether user can vote
            var currentContestContestors = currentContest.Contestors.ToList();
            if (currentContest.VotingStrategy == VotingStrategy.Closed 
                /*&& !currentContestContestors.Contains(user)*/)
            {
                // TODO: Find way to check when VotingStrategy is "Closed" whether user is invited to participate in contest!!!

                resultMessage = "You are not allowed to vote for pictute in this contest";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            // Check whether user has votted for this contest before
            var pictureVotesVoterIds = picture.Votes.Select(v => v.UserId).ToList();
            if (pictureVotesVoterIds.Contains(userId))
            {
                resultMessage = "You are not allowed to vote more than once for pictute";
                return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
            }

            var newVote = new Vote()
            {
                VotedOn = DateTime.Now,
                User = user,
                Picture = picture
            };
            picture.Votes.Add(newVote);
            this.Data.SaveChanges();
            
            return this.Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

    }
}