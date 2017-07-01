using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class Facade : IFacade
    {
        private IUnityContainer _container;
        public Facade(IUnityContainer container)
        {
            this._container = container;
        }

        private IUnitOfWork _unitOfWorkInstance = null;
        private object _syncRoot = new object();

        internal IUnitOfWork UnitOfWorkInstance
        {
            get
            {
                if (_unitOfWorkInstance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_unitOfWorkInstance == null)
                        {
                            _unitOfWorkInstance = this._container.Resolve<IUnitOfWork>(); //GlobalNinjectContainer.Get<IUnitOfWork>();
                        }
                    }
                }

                return _unitOfWorkInstance;
            }
            set
            {
                _unitOfWorkInstance = value;
            }
        }
    }
}
