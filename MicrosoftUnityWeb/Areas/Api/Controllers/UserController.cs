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

        public IFacade FacadeInstance
        {
            get { return _facade; }
        }

        public UserController()
        {

        }

        [HttpPost]
        public IHttpActionResult Add(User user)
        {
            try
            {
                var result = this.FacadeInstance.AddUser(user);
                if(result < 1)
                {
                    return InternalServerError(new Exception("Save Failed"));
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(User user)
        {
            try
            {
                var result = this.FacadeInstance.UpdateUser(user);
                if (result < 1)
                {
                    return InternalServerError(new Exception("Save Failed"));
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(User user)
        {
            try
            {
                var result = this.FacadeInstance.DeleteUser(user);
                if (result < 1)
                {
                    return InternalServerError(new Exception("Save Failed"));
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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
            var users = this.FacadeInstance.GetAllUsers().ToList<User>();
            return users.AsQueryable();
        }

    }
}
