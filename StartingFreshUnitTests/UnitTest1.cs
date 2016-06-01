using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        public void MilestoneIndex_Contains_ListOfMilestones_Model()
        {
            //Arrange
            Mock<IMilestoneRepository> mock = new Mock<IMilestoneRepository>();

            // call to milestone method return this stuff here :::: Doesnt go to DB just returns this 
            mock.Setup(m => m.Milestones).Returns(new MilestoneModel[]
            {
                new MilestoneModel {MilestoneId = 1, Description = "UNIT_TEST", EndDate = DateTime.Today}
            }.AsQueryable());

            MilestoneController controller = new MilestoneController(mock.Object);

            // Act
            var actual = (List<MilestoneModel>) controller.Index(1, 1).Model;

            //Assert
            Assert.IsInstanceOf<List<MilestoneModel>>(actual);

        }









    }
}
