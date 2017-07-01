using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicrosoftUnityWeb.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Users_default",
                "Users/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MicrosoftUnityWeb.Areas.Users.Controllers" }
            );
        }
    }
}