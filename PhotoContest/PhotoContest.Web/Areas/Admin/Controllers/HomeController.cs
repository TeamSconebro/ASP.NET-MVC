using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PhotoContest.Models.Enumerations;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Areas.Admin.Controllers
{   [Authorize(Roles = "Administrator")]
    public class HomeController : BaseAdminController
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {

            var contestDeadlineCheck = this.Data.Contests.All();
            foreach (var contest in contestDeadlineCheck)
            {
                if (contest.Deadline <= DateTime.Now)
                {
                    contest.IsClosed = IsClosed.Yes;

                }
                if (contest.DeadlineStrategy == DeadlineStrategy.ByNumberOfParticipants)
                {
                    if (contest.NumberOfParticipants <= contest.Contestors.Count())
                    {
                        contest.IsClosed = IsClosed.Yes;
                    }
                }
            }

            this.Data.SaveChanges();
            var activeContests = this.Data.Contests.All()
                .Where(c => c.IsClosed == IsClosed.No)
                .OrderByDescending(c => c.CreatedOn)
                .Take(5);

            var activeContestsModel = Mapper.Map<IEnumerable<ContestViewModelHomePage>>(activeContests);

            var inactiveContests = this.Data.Contests.All()
                .Where(c => c.IsClosed == IsClosed.Yes)
                .OrderByDescending(c => c.CreatedOn)
                .Take(5);

            var inactiveContestsModel = Mapper.Map<IEnumerable<ContestViewModelHomePage>>(inactiveContests);

            var compositeViewModel = new List<IEnumerable<ContestViewModelHomePage>>();
            compositeViewModel.Add(activeContestsModel);
            compositeViewModel.Add(inactiveContestsModel);

            return View(compositeViewModel);
        }
    }
}