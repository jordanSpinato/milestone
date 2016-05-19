using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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



        /* Used for Unit Testing with **EFFORT** */
        public DbContextModel(DbConnection connection): base(connection, true)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        /* Used for Unit Testing with **SQL CE** */ 
        public DbContextModel(string connectionString): base("TESTMODEL")//connectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbContextModel(SqlCeConnectionFactory s)
        {
            this.Configuration.LazyLoadingEnabled = false;

        }

    }
}