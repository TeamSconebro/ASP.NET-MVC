namespace PhotoContest.Web.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Data.UnitsOfWork;
    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestData data) : base(data)
        {

        }


        public ActionResult Index()
        {
            return View();
        }
    }
}