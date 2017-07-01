using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftUnityWeb;
using MicrosoftUnityWeb.Controllers;
using DataAccess;
using Microsoft.Practices.Unity;
using System.Web;
using System.IO;
using DataAccess.Fakes;
using Domain;

namespace MicrosoftUnityWeb.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        private StubIFacade _facade;
        //private IFacade _facade;
        private IUnityContainer container;
        public HomeControllerTest()
        {
            container = BootStrapperUnityTest.Initialise();
            this._facade = new StubIFacade();
            //this._facade = new Facade(container);
            this._facade.GetAllUsers = () => new List<User>() { new User() { UserId = 0, FirstName = "Mohammad", LastName = "Zaidi" } }.AsQueryable();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_facade);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(_facade);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(_facade);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Users()
        {
            // Arrange
            HomeController controller = new HomeController(_facade);

            // Act
            ViewResult result = controller.Users() as ViewResult;

            List<User> dataExpected = (List<User>)result.Model;

            Assert.IsNotNull(dataExpected.Count == 1);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");

        }
    }
}
