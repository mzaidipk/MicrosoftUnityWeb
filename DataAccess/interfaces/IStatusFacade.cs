using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IStatusFacade
    {
        IQueryable<Status> GetAllStatus();
    }
}
