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
        public int UpdateUser(User user)
        {
            this.UnitOfWorkInstance.UserRepository.Update(user);
            return this.UnitOfWorkInstance.Save();
        }

        public int DeleteUser(User user)
        {
            this.UnitOfWorkInstance.UserRepository.Delete(user);
            return this.UnitOfWorkInstance.Save();
        }

        public int AddUser(User user)
        {
            this.UnitOfWorkInstance.UserRepository.Insert(user);
            return this.UnitOfWorkInstance.Save();
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.UnitOfWorkInstance.UserRepository.GetAll(filter: null, orderBy: "", includes: "");
        }

        public IQueryable<User> GetUserById(int Id)
        {
            return this.UnitOfWorkInstance.UserRepository.GetAll(filter: s => s.UserId == Id, orderBy: "", includes: "");
        }
    }
}
