using DataAccess;
using DataAccess.Fakes;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using MicrosoftUnityWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MicrosoftUnityWeb.Tests
{
    public class BootStrapperUnityTest
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit  
            //container.RegisterType<ICompanyRepository, CompanyRepository>();



            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType(typeof(IControllerActivator), typeof(UnityControllerActivator));
            //container.RegisterType<IFacade, Facade>();
            container.RegisterType<StubIFacade, StubIFacade>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
