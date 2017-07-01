using DataAccess.App_Start;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;


namespace MicrosoftUnityWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private static IUnityContainer container;


        ///// <summary>
        ///// Register unity 
        ///// </summary>
        //private static void RegisterIoC()
        //{
        //    if (container == null)
        //    {
        //        container = CreateUnityContainer();
        //    }
        //}

        ///// <summary>
        ///// Create the unity container
        ///// </summary>
        //private static IUnityContainer CreateUnityContainer()
        //{
        //    container = UnityWebActivator.Container;
        //    RegisterTypes();

        //    return container;
        //}

        //public static void RegisterTypes()
        //{
        //    //unityContainer.RegisterType<BaseDbContext>(new PerRequestLifetimeManager());
        //}


        /// <summary>
        /// Change MVC Configuration
        /// </summary>
        private static void ChangeMvcConfiguration()
        {
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new MPC.MIS.App_Start.CustomRazorViewEngine());
        }

        protected void Application_Start()
        {
            //UnityWebActivator.Start();
            BootStrapperUnity.Initialise();
            //ChangeMvcConfiguration();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
