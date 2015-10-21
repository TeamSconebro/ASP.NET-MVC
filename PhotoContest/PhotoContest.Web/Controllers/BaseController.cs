namespace PhotoContest.Web.Controllers
{
    using System.Web.Mvc;
    using Data.UnitsOfWork;
    public class BaseController : Controller
    {
        public BaseController(IPhotoContestData data)
        {
            this.Data = data;
        }

        public BaseController()
        {

        }

        protected IPhotoContestData Data { get; set; }
    }
}