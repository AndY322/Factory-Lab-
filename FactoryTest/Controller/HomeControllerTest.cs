using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factory.Controllers;
using System.Web.Mvc;

namespace FactoryTest.Controller
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index(null, null, null, null) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index(null, null, null, null) as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexStringInViewbag()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index("some search string", null, null, null) as ViewResult;

            Assert.AreEqual("some search string", result.ViewBag.searchString);
        }
    }
}
