using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.UIModels;

namespace DataAccess
{
    public partial class Facade
    {
        public IQueryable<Sample> GetAllSamples()
        {
            return this.UnitOfWorkInstance.SampleRepository.GetAll(filter: null, orderBy: "", includes: "");
        }

        public void AddUserAndSamples(UserSamples userSamples)
        {
            this.UnitOfWorkInstance.UserRepository.Insert(userSamples.UserData);
            this.UnitOfWorkInstance.SampleRepository.Insert(userSamples.Samples);

            this.UnitOfWorkInstance.Save();
        }


    }
}
