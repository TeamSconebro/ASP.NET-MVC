using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Models;
using PhotoContest.Models.Enumerations;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Controllers
{
    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) : base(data)
        {
        }
        [ActionName("Active")]
        public ActionResult AllActiveContests()
        {
            var activeContests = this.Data.Contests
                .All()
                .Where(c=>c.IsClosed==IsClosed.No)
                .OrderByDescending(c => c.CreatedOn);
            //.Project()
            //.To<ContestViewModel>();
            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(activeContests);
            return this.View(contestModels);
        }
        [ActionName("Inactive")]
        public ActionResult AllInactiveContests()
        {
            var inactiveContests = this.Data.Contests
               .All()
               .Where(c => c.IsClosed == IsClosed.Yes)
               .OrderByDescending(c => c.CreatedOn);
            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(inactiveContests);
            return this.View(contestModels);
            
        }

        // List all active/inactive contests with more details (after pressing "See more" button)
        public ActionResult AllContests()
        {
            var allContests = this.Data.Contests
              .All()
              .OrderByDescending(c => c.CreatedOn);
            var contestModels = Mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>(allContests);
            return this.View(contestModels);
        }
        [HttpGet]
        public ActionResult ContestDetails(long id=98567457552123554)
        {
            if (id == 98567457552123554)
            {
                id = this.Data.Contests.All().OrderByDescending(c => c.CreatedOn).Select(c=>c.Id).FirstOrDefault();
            }
            var contestDetails = this.Data.Contests.Find(id);
            var contestModels = Mapper.Map<Contest,ContestViewModel>(contestDetails);
            //TODO: Return view model with all detals for one particular contest. That view is rendered on Contest Details page in Visitors, Logged users and Administrators accounts.

            return this.View(contestModels);
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