using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Domain;
using System.Web.Script.Serialization;

namespace MicrosoftUnityWeb.Controllers
{

    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private IFacade _facade;
        public HomeController(IFacade facade)
        {
            this._facade = facade;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Users()
        {
            var users = this._facade.GetAllUsers().ToList();
            //users = null;
            return View(users);
        }

        [Route("Samples/{id:int}")]
        public ActionResult Samples(int id)
        {
            var samples = this._facade.GetAllSamples().Where(s => s.CreatedBy == id).ToList();
            return View(samples);
        }

        public string GetUsers()
        {
            var result = this._facade.GetAllUsers().ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(result);
            return json;
        }
    }
}