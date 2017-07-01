using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
//using DataAccess;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UserRepository { get; }
        IBaseRepository<Sample> SampleRepository { get; }
        IBaseRepository<Status> StatusRepository { get; }

        void Save();

        void Dispose();


    }
}
