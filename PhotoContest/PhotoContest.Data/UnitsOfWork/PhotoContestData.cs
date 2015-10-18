using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Data.Repositories;
using PhotoContest.Models;

namespace PhotoContest.Data.UnitsOfWork
{
    public class PhotoContestData : IPhotoContestData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public PhotoContestData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Contest> Contests
        {
            get { return this.GetRepository<Contest>(); }
        }

        public IRepository<ContestPicture> ContestPictures
        {
            get { return this.GetRepository<ContestPicture>(); }
        }

        public IRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }
        }

        public IRepository<Notification> Notifications
        {
            get { return this.GetRepository<Notification>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public int SaveChangesAsync()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
                var repository = Activator.CreateInstance(typeOfRepository, this.context);

                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
