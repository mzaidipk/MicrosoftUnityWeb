using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Domain;

namespace MicrosoftUnityWeb.Areas.Users.Controllers
{
    public class HomeController : Controller
    {
        private IFacade _facade;
        public HomeController(IFacade facade)
        {
            this._facade = facade;
        }

        //public JsonResult GetUsers()
        //{
        //    var users = this._facade.GetAllUsers().ToList<User>();
        //    return new JsonResult() { Data= users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        // GET: Users/Home
        public ActionResult Index()
        {
            ViewBag.Title = "Test";
            return View();
        }
    }
}