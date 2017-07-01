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
    //[RoutePrefix("User")]
    public class UserController : ApiController
    {
        private IFacade _facade;

        public UserController()
        {

        }

        [HttpGet]
        //[Route("/User/Get")]
        public IQueryable<User> Get()
        {
            return new List<User>() { new User() { UserId = 5, FirstName = "Rio", LastName = "Test" } }.AsQueryable();
        }

        public UserController(IFacade facade)
        {
            this._facade = facade;
        }

        [HttpGet]
        //[Route("User/GetAllUsers")]
        public IQueryable<User> GetAllUsers()
        {
            var users = this._facade.GetAllUsers().ToList<User>();
            return users.AsQueryable();
        }

    }
}
