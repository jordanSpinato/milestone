using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using StartingFresh;
using StartingFresh.Controllers;
using StartingFresh.Models;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace StartingFresh.Tests.Controllers {

    //[TestClass]
    //public class HomeControllerTest {
    //    [TestMethod]
    //    public void Index() {
    //        // Arrange
    //        HomeController controller = new HomeController();

    //        // Act
    //        ViewResult result = controller.Index() as ViewResult;

    //        // Assert
    //        Assert.IsNotNull(result);
    //    }

    //    [TestMethod]
    //    public void About() {
    //        // Arrange
    //        HomeController controller = new HomeController();

    //        // Act
    //        ViewResult result = controller.About() as ViewResult;

    //        // Assert
    //        Assert.AreEqual("Your application description page.", result.ViewBag.Message);
    //    }

    //    [TestMethod]
    //    public void Contact() {
    //        // Arrange
    //        HomeController controller = new HomeController();

    //        // Act
    //        ViewResult result = controller.Contact() as ViewResult;

    //        // Assert
    //        Assert.IsNotNull(result);
    //    }
    //}


    [TestFixture]
    public class MilestoneControllerTest
    {

        [Test]
        public void Display_Create_Page()
        {
            // arrange
            MilestoneController controller = new MilestoneController();

            // act
            ViewResult result = controller.Create() as ViewResult;

            //assert
            Assert.AreEqual("Create Init", result.ViewBag.Message);
        }


        [Test]
        public void Create_Returns_ActionResult()
        {
            var controller = new MilestoneController();

            var actual = controller.Create();

            Assert.IsInstanceOf<ActionResult>(actual);

        }

        [Test]
        public void Index_Returns_ActionResult() {
            var controller = new MilestoneController();

            var actual = controller.Index(1,1);

            Assert.IsInstanceOf<ActionResult>(actual);

        }

        [Test]
        public void Edit_Returns_ActionResult() {
            var controller = new MilestoneController();

            var actual = controller.Edit(1);

            Assert.IsInstanceOf<ActionResult>(actual);

        }

        [Test]
        public void Details_Returns_ActionResult() {
            var controller = new MilestoneController();

            var actual = controller.Details(2);

            Assert.IsInstanceOf<ActionResult>(actual);

        }

        [Test]
        public void Delete_Returns_ActionResult() {
            var controller = new MilestoneController();

            var actual = controller.Delete(1);

            Assert.IsInstanceOf<ActionResult>(actual);

        }











    }
}
