using DataAccess;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using MicrosoftUnityWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MicrosoftUnityWeb
{
    public class BootStrapperUnity
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new Microsoft.Practices.Unity.WebApi.UnityDependencyResolver(container);

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
            container.RegisterType<IFacade, Facade>();
            container.RegisterType<genebygene2017Entities, genebygene2017Entities>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
        }
    }
}