/*
 * Author: jordan@enteracloud.com
 * 
 * 
 * 
 * For Better testing I will remove database access
 *  Implement: 
 *  1- Repositories
 *  2- Mocks
 *  3- Dependency Injection 
 * */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using StartingFresh.Models;
using FormCollection = System.Web.Mvc.FormCollection;

namespace StartingFresh.Controllers
{
    public class MilestoneController : Controller
    {

        public DbContextModel DbContext { get; set; }

        // DbContextModel DbContext = new DbContextModel();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DbContext = new DbContextModel();
            base.OnActionExecuting(filterContext);
        }



        // GET: Milestone
        public ActionResult Index(int? id, int? x)
        {
            ViewBag.Code = 0; // nothing
            ViewBag.Message = "Index Init";

            MilestoneModel model = new MilestoneModel();
            model.Description = "";
            model.EndDateString = "";
            model.EndDate = DateTime.Now;
            model.StartTimeString = "";
            model.StartTime = DateTime.Now;
            model.TotalProjectDays = 0;

            if (x == 99)
            {
                ViewBag.Message = "Index Init99";
                return View("Create");
            }

         
            try
            {
                
                model.Milestones = DbContext.Milestones.ToList();

                if (id == null)
                {
                    Console.WriteLine("NULL ID");
                    return View(model);
                }
                return View(model); //dbContext.Milestones.ToList()); //dbContext.Milestones.ToList());
            }


            catch (Exception e)
            {
                Console.WriteLine("$$$$$");
                return RedirectToAction("Create");

            }


            return View(model); //dbContext.Milestones.ToList()); //dbContext.Milestones.ToList());


        }



        public ActionResult Create()
        {
            ViewBag.Message = "Create Init";

            MilestoneModel model = new MilestoneModel();

            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(MilestoneModel model)
        {
            ViewBag.Message = "Create Post";


            try
            {
                DateTime today = DateTime.Now;
                TimeSpan range = new TimeSpan();

                if (ModelState.IsValid)
                {
                    // model.Description is already set

                    model.StartTime = today;
                    model.StartTimeString = today.ToString("D");

                    // model.EndDate is already set
                    model.EndDateString = model.EndDate.ToString("D");

                    range = model.EndDate - today;
                    var totalDays = range.Days;

                    model.TotalProjectDays = totalDays + 1;

                    if (model.TotalProjectDays <= 0)
                    {
                        model.TotalProjectDays = 0;
                    }

                    TempData["SuccessMessage"] = " has been added.";
                    TempData["Date"] = model.EndDateString;
                    TempData["Description"] = model.Description;

                    DbContext.Milestones.Add(model);
                    DbContext.SaveChanges();


                    ViewBag.Message = "success";

                    return RedirectToAction("Index");
                }

                return View(model);
            }


            catch (Exception e)
            {
                ViewBag.Message = "fail";

                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(int? id)
        {
            ViewBag.Message = "Edit Init";

            MilestoneModel model = DbContext.Milestones.Find(id);
           
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (model == null)
                return HttpNotFound();

            return View(model);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)//, MilestoneModel newModel)
        {
            ViewBag.Message = "Edit Post";

            DateTime today = DateTime.Now;
            TimeSpan range = new TimeSpan();



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MilestoneModel databaseModel = DbContext.Milestones.Find(id);
           

            if (TryUpdateModel(databaseModel, "", new string[] {"Description", "EndDate"}))
            {
                databaseModel.EndDateString = databaseModel.EndDate.ToString("D");


                range = databaseModel.EndDate - today;
                var totalDays = range.Days;

                databaseModel.TotalProjectDays = totalDays + 1;

                if (databaseModel.TotalProjectDays <= 0)
                {
                    databaseModel.TotalProjectDays = 0;
                }


                try
                {
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }

            return View(databaseModel);
        }
        
        public ActionResult Details(int? id)
        {
            ViewBag.Message = "Details Init";

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MilestoneModel model = DbContext.Milestones.Find(id);
            

            if (model == null)
                return HttpNotFound();

            return View(model);

        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            ViewBag.Message = "Delete Init";

            if (id== null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MilestoneModel model = DbContext.Milestones.Find(id);

            if (model == null)
                return HttpNotFound();
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ViewBag.Message = "Delete Post";


            try {
                MilestoneModel model = DbContext.Milestones.Find(id);
                DbContext.Milestones.Remove(model);
                DbContext.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new {id = id, saveChangesError = true});
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult DeleteSelected(FormCollection formCollection)
        {
            ViewBag.Message = "Delete Selected Post";

            MilestoneModel model = new MilestoneModel();
            var deletedMilestones = 0;
            try
            {
                string[] idsToDelete = formCollection["milestoneID"].Split(new char[] {','});

                foreach (string id in idsToDelete)
                {
                    var deleteID = this.DbContext.Milestones.Find(int.Parse(id));
                    this.DbContext.Milestones.Remove(deleteID);
                    deletedMilestones++;

                    this.DbContext.SaveChanges();
                    TempData["deletedMilstoneCount"] = deletedMilestones;
                }
                return RedirectToAction("Index");
            }

            catch (Exception e)
            {

                TempData["NothingSelected"] = "No Milestones were selected";
                return RedirectToAction("Index");
            }
          return RedirectToAction("Index");

        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}