using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Web.Models.BindingModel;

namespace PhotoContest.Web.Controllers
{
    [Authorize]
    public class VotesController : BaseController
    {
        public VotesController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Display votting button
        public ActionResult Vote()
        {
            // TODO: Return view model for displaying voting button

            return View();
        }

        // POST: Store vote in database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteBindingModel model)
        {
            // TODO: Recieve binding model and increment votes for one particular picture.

            return RedirectToAction("ContestDetails", "Contests");
        }

    }
}