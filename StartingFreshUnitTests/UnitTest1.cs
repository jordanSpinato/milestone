using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartingFresh.Controllers;
using StartingFresh.Models;

namespace StartingFreshUnitTests {

    [TestClass]
    public class UnitTest1 {

        public MilestoneModel model = new MilestoneModel();
        public MilestoneController controller = new MilestoneController();
        private DbContextModel context = new DbContextModel();



        [TestMethod]
        public void SetupController()
        {

           model.Description = "test";
           model.EndDate = DateTime.Today;
           controller.Create(model);
            
           var x = controller.Create(model);

            //var x = controller.Details(model.MilestoneId);
            Console.WriteLine(model.Description);
           // Assert.IsNotNull(context);

            
        }


        [TestMethod]
        public void TestInitCreateView()
        {
            MilestoneController controller2 = new MilestoneController();
            var res = controller.Details(2) as ViewResult;

            MilestoneModel model1 = new MilestoneModel();

            var result = controller.Create() as ViewResult;
            var result2 = controller.Create(model) as ViewResult;

            Assert.IsNotNull(result2);

            Assert.AreEqual("Create",result.ViewName);
            Assert.AreEqual("Create", result2.ViewName);

        }
        
    }
}
