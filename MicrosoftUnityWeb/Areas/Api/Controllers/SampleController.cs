using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MicrosoftUnityWeb.Areas.Api.Controllers
{
    public class SampleController : ApiController
    {
        private IFacade _facade;
        public Facade FacadeInstance { get; set; }

        public SampleController()
        {

        }
        public SampleController(IFacade facade)
        {
            this._facade = facade;
        }

        public IQueryable <Sample> Get()
        {
            return FacadeInstance.GetAllSamples().ToList().AsQueryable();
        }


    }
}
