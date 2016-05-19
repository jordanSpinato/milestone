// Unit Testing
// - Arrange - Preconditions and inputs
// - Act     - Act on object or method under test  
// - Assert  - Expected results occured 

using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMemory;
using StartingFresh.Controllers;
using StartingFresh.Models;

namespace StartingFreshUnitTests {

    [TestClass]
    public class UnitTest1
    {
        //private DbContextModel _context;
      //  DbConnection connection = Effort.DbConnectionFactory.CreateTransient();


        //public MilestoneModel model = new MilestoneModel();
        //private DbContextModel context = new DbContextModel();


        [TestMethod]
        public void IndexView()
        {
            MilestoneController controller = new MilestoneController();
            ViewResult result = controller.Index(1,99) as ViewResult;

            var indexCheck = "Index Init99";

            // Act

            // Assert
            Assert.AreEqual(indexCheck, result.ViewBag.Message);
        }








        [TestMethod]
        public void setup()
        {
            DbContextModel _context;
            MilestoneModel model = new MilestoneModel();
            MilestoneController controller = new MilestoneController();

            var filePath = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            if (File.Exists(filePath))
                File.Delete(filePath);

            // create SQL CE Connection string which essentially points to the filepath
            string connectionString = filePath;

            // Set this to make database creation work with SQL CE
            SqlCeConnectionFactory DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            // Create the database with the schema from the entity model
            var context = new DbContextModel(filePath);
           // context.Database.Create();

            // initialize the DbContext class with the SQL CE connection string
            _context = new DbContextModel(connectionString);

            model.Description = "Test2";
            model.MilestoneId = 2;
            model.EndDate = DateTime.Today;

            _context.Milestones.Add(model);

            model.Milestones = _context.Milestones.ToList();

            controller.Index(1,99);

            Assert.IsNotNull(model);









        }







        //[TestInitialize]
        [TestMethod]
        public void SetupTest()
        {
            // C:\Users\jordan\MilestoneDatabase.mdf -- location of DB

            // Context Model used in the entity framework
            //DbContextModel _context;

            // Create a connection from the EFFORT framework to test the connection
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();

            // Create the entity "mock database" from the previously created connection
            DbContextModel context = new DbContextModel(connection);

            MilestoneController controller = new MilestoneController();

            //
            //context.Milestones.Add(new MilestoneModel() {MilestoneId = 1});
            // _context = new DbContextModel(connection);

            MilestoneModel model = new MilestoneModel();
            MilestoneModel mode2 = new MilestoneModel();

            var id = 1;

            model.MilestoneId = 1;
            model.Description = "Test1";
            model.EndDate = new DateTime(2);

            context.Milestones.Add(model);

            model.Milestones = context.Milestones.ToList();


            controller.Create(model);


            Assert.IsNotNull(model.Milestones);

                // Add 2 Product entities
                context.Milestones.Add(model);

            var des = context.Milestones.Find(model.MilestoneId);
            Console.WriteLine(des.EndDateString);

            Assert.IsNotNull(context);

        }



        //[TestMethod]
        //public void Create()
        //{
        //    using (DbContextModel context = new DbContextModel(connection)) {
        //        // Add 2 Product entities
        //        context.Milestones.Add(new MilestoneModel())
        //        {
        //            MilestoneId = 1,
        //            MilestoneDescription = "Test 1",
        //        }
        //        )

                /*context.Products.Add(new Product() {
                    Id = new Guid("A4A989A4-..."),
                    Name = "Product 2", ...
                    */
                    /*
                // Add 3 Tag entities
                context.Tags.Add(new Tag() {
                    Id = new Guid("D7FE98A2-..."),
                    Name = "Tag 1", ...
                context.Tags.Add(new Tag() {
                    Id = new Guid("52FEDB17-..."),
                    Name = "Tag 2", ...
                context.Tags.Add(new Tag() {
                    Id = new Guid("45312740-..."),
                    Name = "Tag 3", ...

                // Associate Product 1 with Tag 1
                context.ProductTags.Add(new ProductTag() {
                    ProductId = new Guid("CEA4655C-..."),
                    TagId = new Guid("D7FE98A2-...")
                    ...
            //*/
            //    });

            //    // Associate Product 1 with Tag 3
            //    context.ProductTags.Add(new ProductTag() {
            //        ProductId = new Guid("CEA4655C-..."),
            //        TagId = new Guid("45312740-...")
            //         ...
            //    });

            //    // Associate Product 2 with Tag 2
            //    context.ProductTags.Add(new ProductTag() {
            //        ProductId = new Guid("A4A989A4-..."),
            //        TagId = new Guid("52FEDB17-...")
            //        ...
            //    });
           




        [TestMethod]
        public void CheckIndexInitialize()
        {
           string viewBagMessageCheck = "Index Init";
            
           // Arrange
           MilestoneController controller = new MilestoneController();

           // Act
           ViewResult result = controller.Index(1,99) as ViewResult;

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
