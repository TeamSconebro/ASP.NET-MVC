using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Models;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Controllers
{
    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) : base(data)
        {
        }

        public ActionResult AllActiveContests()
        {
            // TODO: Return view model with all active contests, order by date of creation (descending). That view is rendered on Home page - Visitors, Home page - Logged users, Home page - Administrators.
            var activeContests = this.Data.Contests
                .All()
                .OrderByDescending(c => c.Deadline)
                .Project()
                .To<ContestViewModel>();
            return this.View(activeContests);
        }

        public ActionResult AllInactiveContests()
        {
            // TODO: Return view model with all inactive contests, order by date of creation (descending). That view is rendered on Home page - Visitors, Home page - Logged users, Home page - Administrators.

            return this.View();
        }

        // List all active/inactive contests with more details (after pressing "See more" button)
        public ActionResult AllContests()
        {
            // TODO: Return view model with all active/inactive contests, order by date of creation (descending). That view is rendered on Contests page in Visitors, Logged users and Administrators accounts.

            return this.View();
        }

        public ActionResult ContestDetails()
        {
            //TODO: Return view model with all detals for one particular contest. That view is rendered on Contest Details page in Visitors, Logged users and Administrators accounts.

            return this.View();
        }

        public ActionResult CreateContest()
        {
            // TODO: Recieve binding model and create new contest in database.

            return this.View();
        }

        public ActionResult EditContest()
        {
            // TODO: Recieve binding model and modify existing contest.

            return this.View();
        }

        public ActionResult DeleteContest()
        {
            // TODO: Delete contest by id.

            return this.View();
        }
    }
}