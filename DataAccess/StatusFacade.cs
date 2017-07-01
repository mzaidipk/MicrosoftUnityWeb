using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class Facade
    {
        public IQueryable<Status> GetAllStatus()
        {
            return this.UnitOfWorkInstance.StatusRepository.GetAll(filter: null, orderBy: "", includes: "");
        }
    }
}
