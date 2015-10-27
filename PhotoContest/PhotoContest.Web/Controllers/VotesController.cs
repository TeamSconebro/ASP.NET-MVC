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
        public ActionResult Vote(int id)
        {
            // TODO: Recieve picure id and increment votes for one particular picture.
            var currentPicture = this.Data.ContestPictures.Find(id);
            if (currentPicture == null)
            {
                return HttpNotFound();
            }

            currentPicture.VotesCount++;
            this.Data.SaveChanges();

            return RedirectToAction("ContestDetails", "Contests");
        }

    }
}