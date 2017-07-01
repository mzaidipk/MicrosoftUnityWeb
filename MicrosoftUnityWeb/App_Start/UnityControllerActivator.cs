using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MicrosoftUnityWeb.App_Start
{
    public class UnityControllerActivator : IControllerActivator
    {
        #region Public

        /// <summary>
        /// Creates a controller.
        /// </summary>
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }

        #endregion
    }
}