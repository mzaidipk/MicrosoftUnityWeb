using Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUnityContainer container)
        {
            dbContext = container.Resolve<genebygene2017Entities>();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        private genebygene2017Entities dbContext;
        private IBaseRepository<User> userRepository;
        private IBaseRepository<Sample> sampleRepository;
        private IBaseRepository<Status> statusRepository;

        private bool _disposed = false;

        public IBaseRepository<Sample> SampleRepository
        {
            get
            {
                if (this.sampleRepository == null)
                {
                    this.sampleRepository = new BaseRepository<Sample>(this.dbContext);
                }
                return sampleRepository;
            }
        }

        public IBaseRepository<Status> StatusRepository
        {
            get
            {
                if (this.statusRepository == null)
                {
                    this.statusRepository = new BaseRepository<Status>(this.dbContext);
                }
                return statusRepository;
            }
        }

        public IBaseRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new BaseRepository<User>(this.dbContext);
                }
                return userRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //if (_entities != null)
                    //    _entities.Dispose();

                }
            }
            this._disposed = true;
        }

        public int Save()
        {
            return this.dbContext.SaveChanges();
        }
    }
}
