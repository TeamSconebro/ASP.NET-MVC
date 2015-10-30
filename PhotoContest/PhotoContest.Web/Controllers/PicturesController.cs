using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoContest.Data.UnitsOfWork;
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

        [HttpPost]
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

        public ActionResult Vote(int id)
        {
            var picture = this.Data.ContestPictures.Find(id);
            if (picture == null)
            {
                //return this.HttpNotFound();
                return this.Json(false, JsonRequestBehavior.AllowGet);
            }

            // TODO: Check if current user is logged, is author of the contests and is he votted yet.

            picture.VotesCount++;
            this.Data.SaveChanges();

            //return this.Json(new { success = "Success" }, JsonRequestBehavior.AllowGet);
            //return this.Content("SUCCESS");
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}