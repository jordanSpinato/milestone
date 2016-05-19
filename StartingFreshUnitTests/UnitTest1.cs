// Unit Testing
// - Arrange - Preconditions and inputs
// - Act     - Act on object or method under test  
// - Assert  - Expected results occured 

using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartingFresh.Controllers;
using StartingFresh.Models;

namespace StartingFreshUnitTests {

    [TestClass]
    public class UnitTest1 {

        public MilestoneModel model = new MilestoneModel();
        private DbContextModel context = new DbContextModel();
        
        [TestMethod]
        public void CheckIndexInitialize()
        {
           string viewBagMessageCheck = "Index Init";
            
           // Arrange
           MilestoneController controller = new MilestoneController();

           // Act
           ViewResult result = controller.Index(1) as ViewResult;

           // Assert
           Assert.AreEqual(viewBagMessageCheck, result.ViewBag.Message);

        }

    [TestMethod]
        public void DetailsView() {

        }

        [TestMethod]
        public void DeleteView() {

        }

        [TestMethod]
        public void EditView() {

        }


    }
}
