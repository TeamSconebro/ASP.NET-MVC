using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoContest.Models.Enumerations;
using PhotoContest.Web.Models.ViewModels;

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
            var contestDeadlineCheck = this.Data.Contests.All();
            foreach (var contest in contestDeadlineCheck)
            {
                if (contest.Deadline <= DateTime.Now)
                {
                    contest.IsClosed = IsClosed.Yes;
                    
                }
                if (contest.DeadlineStrategy==DeadlineStrategy.ByNumberOfParticipants)
                {
                    if (contest.NumberOfParticipants<=contest.Contestors.Count())
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