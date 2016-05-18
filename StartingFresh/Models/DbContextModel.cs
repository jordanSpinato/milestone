using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StartingFresh.Models
{
    public class DbContextModel : DbContext
    {
        public DbContextModel() : base("MilestoneModel")
        {

            Database.SetInitializer<DbContextModel>(new DropCreateDatabaseIfModelChanges<DbContextModel>());
        }

        public DbSet<MilestoneModel> Milestones  { get; set; }
        


    }
}