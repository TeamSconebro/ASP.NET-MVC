using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoContest.Data.UnitsOfWork;

namespace PhotoContest.Web.Controllers
{
    public class PicturesController : BaseController
    {
        public PicturesController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Pictures
        public ActionResult AllPictures()
        {
            //TODO: Return view model with all pictures in one particular contest. Model should contain picture title, content (image) and author. That model should be rendered on the Contest details page.

            return View();
        }

    }
}