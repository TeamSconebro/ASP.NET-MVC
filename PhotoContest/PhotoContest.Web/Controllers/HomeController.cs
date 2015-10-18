using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoContest.Data;
using PhotoContest.Data.UnitsOfWork;

namespace PhotoContest.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestData data) : base(data)
        {

        }

        public HomeController() : this(new PhotoContestData(new PhotoContestContext()))
        {

        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}