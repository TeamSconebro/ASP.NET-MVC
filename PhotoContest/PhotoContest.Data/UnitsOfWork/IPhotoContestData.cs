using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Data.Repositories;
using PhotoContest.Models;

namespace PhotoContest.Data.UnitsOfWork
{
    public interface IPhotoContestData
    {
        IRepository<User> Users { get; }

        IRepository<Contest> Contests { get; }

        IRepository<ContestPicture> ContestPictures { get; }

        IRepository<Vote> Votes { get; }

        IRepository<Notification> Notifications { get; }

        int SaveChanges();
    }
}
