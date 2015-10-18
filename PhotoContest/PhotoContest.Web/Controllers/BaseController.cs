using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoContest.Data.UnitsOfWork;

namespace PhotoContest.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IPhotoContestData data)
        {
            this.Data = data;
        }

        public BaseController()
        {

        }

        protected IPhotoContestData Data { get; }
    }
}