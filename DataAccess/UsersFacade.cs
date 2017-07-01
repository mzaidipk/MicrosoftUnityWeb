using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess
{
    public partial class Facade
    {
        public IQueryable<User> GetAllUsers()
        {
            return this.UnitOfWorkInstance.UserRepository.GetAll(filter: null, orderBy: "", includes: "");
        }
    }
}
