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
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using NMemory.Linq;
using StartingFresh.Models;
using FormCollection = System.Web.Mvc.FormCollection;

namespace StartingFresh.Controllers
{
    /* this is a different branch */

    public class MilestoneController : Controller
    {

        //public DbContextModel DbContext { get; set; }

        private DbContextModel DbContext = new DbContextModel();
        private IMilestoneRepository milestoneRepo;


        public MilestoneController() {
            this.milestoneRepo = new EfMilestoneRepository();
        }


        //create constructor for the property for the milestone Repo
        public MilestoneController(IMilestoneRepository milestoneRepo)
        {
            this.milestoneRepo = milestoneRepo;
        }
            


        // GET: Milestone
        public ActionResult Index(string sortOrder, string searchString, string currentFilter) //int? id, int? x)
        {
            MilestoneModel model = new MilestoneModel();

            // if sortOrder == 'Description' then set viewbag.descriptionSortParam to 'description_desc' else set viewbag.DescriptionSortParam to 'description'
            ViewBag.EndDateSortParam = String.IsNullOrEmpty(sortOrder) ? "endDate_desc" : "";
            ViewBag.DescriptionSortParam = sortOrder == "Description" ? "description_desc" : "Description";
            ViewBag.StartDateSortParam = sortOrder == "StartDate" ? "startDate_desc" : "StartDate";
            ViewBag.ProjectDaysSortParam = sortOrder == "ProjectDays" ? "projectDays_desc" : "ProjectDays";
            ViewBag.DaysRemainingSortParam = sortOrder == "DaysRemaining" ? "daysRemaining_desc" : "DaysRemaining";

            model.Milestones = milestoneRepo.Milestones.ToList(); // pulls from database

            var query = from s in milestoneRepo.Milestones select s;

            foreach (var item in query)
            {
                var updatedModel = DbContext.Milestones.Single((x => x.MilestoneId == item.MilestoneId));
                var todayDate = DateTime.Now;
                var range = item.EndDate - todayDate;
                var daysLeft = range.Days + 1;


                updatedModel.DaysRemaining = daysLeft;
                model.DaysRemaining = updatedModel.DaysRemaining;

                DbContext.SaveChanges();
            }


            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRRRR");
            }
        
            ViewBag.CurrentFilter = searchString;

            // Check for a string to search and display the results. Not case sensitive
            if (!String.IsNullOrEmpty(searchString)) 
            {
                model.Milestones = model.Milestones.Where(s => s.Description.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder) {
                case "Description":
                    model.Milestones = model.Milestones.OrderBy(s => s.Description);
                    break;
                case "description_desc":
                    model.Milestones = model.Milestones.OrderByDescending(s => s.Description);
                    break;
                case "StartDate":
                    model.Milestones = model.Milestones.OrderBy(s => s.StartTimeString);
                    break;
                case "startDate_desc":
                    model.Milestones = model.Milestones.OrderByDescending(s => s.StartTimeString);
                    break;
                case "ProjectDays":
                    model.Milestones = model.Milestones.OrderBy(s => s.TotalProjectDays);
                    break;
                case "projectDays_desc":
                    model.Milestones = model.Milestones.OrderByDescending(s => s.TotalProjectDays);
                    break;
                case "DaysRemaining":
                    model.Milestones = model.Milestones.OrderBy(s => s.DaysRemaining);
                    break;
                case "daysRemaining_desc":
                    model.Milestones = model.Milestones.OrderByDescending(s => s.DaysRemaining);
                    break;
                case "endDate_desc":
                    model.Milestones = model.Milestones.OrderByDescending(s => s.EndDateString);
                    break;
                default:
                    model.Milestones = model.Milestones.OrderBy(s => s.EndDateString);
                    break;
            }



    return View(model);//milestoneRepo.Milestones.ToList()); //model); 

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
                DateTime yesterday = new DateTime(2016, 6, 2);

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
                    model.DaysRemaining = totalDays + 1;

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
                var daysLeft = range.Days + 1;

                databaseModel.DaysRemaining = daysLeft;

                DbContext.SaveChanges();

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