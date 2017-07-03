using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public interface IUserFacade
    {
        IQueryable<User> GetAllUsers();
        int AddUser(User user);
        int UpdateUser(User user);
        int DeleteUser(User user);
        IQueryable<User> GetUserById(int Id);
    }
}
