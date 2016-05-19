/*
 * Author: jordan@enteracloud.com
 * */
using System;
using System.Collections.Generic;
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

        private DbContextModel dbContext = new DbContextModel();
        
        // GET: Milestone
        public ActionResult Index(int? id)
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
            
            model.Milestones = dbContext.Milestones.ToList();

            if (id == null)
            {
                Console.WriteLine("NULL ID");
                return View(model);
            }
            return View(model);//dbContext.Milestones.ToList()); //dbContext.Milestones.ToList());
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

                dbContext.Milestones.Add(model);            
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        public ActionResult Edit(int? id)
        {
            ViewBag.Message = "Edit Init";

            MilestoneModel model = dbContext.Milestones.Find(id);
           
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

            MilestoneModel databaseModel = dbContext.Milestones.Find(id);
           

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
                    dbContext.SaveChanges();
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

            MilestoneModel model = dbContext.Milestones.Find(id);
            

            if (model == null)
                return HttpNotFound();

            return View(model);

        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            ViewBag.Message = "Delete Init";

            if (id== null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MilestoneModel model = dbContext.Milestones.Find(id);

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
                MilestoneModel model = dbContext.Milestones.Find(id);
                dbContext.Milestones.Remove(model);
                dbContext.SaveChanges();
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
                    var deleteID = this.dbContext.Milestones.Find(int.Parse(id));
                    this.dbContext.Milestones.Remove(deleteID);
                    deletedMilestones++;

                    this.dbContext.SaveChanges();
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
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}