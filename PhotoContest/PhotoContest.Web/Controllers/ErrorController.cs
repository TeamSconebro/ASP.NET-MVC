using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoContest.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [ActionName("404")]
        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            return this.View("Error");
        }
    }
}