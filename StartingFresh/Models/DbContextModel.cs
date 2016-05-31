using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace StartingFresh.Models
{
    public class DbContextModel : DbContext, IDbContextModel
    {
        public DbContextModel()
          : base(ConfigurationManager.ConnectionStrings["MilestoneModel"].ConnectionString)
        {
        //    Database.SetInitializer<DbContextModel>(new DropCreateDatabaseIfModelChanges<DbContextModel>());
        }

        public DbContextModel(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public IDbSet<MilestoneModel> Milestones { get; set; }
        
    }


    public interface IDbContextModel
    {
        IDbSet<MilestoneModel> Milestones { get; set; }

    }



    /*
    /* Used for Unit Testing with **EFFORT** 
    public DbContextModel(DbConnection connection): base(connection, true)
    {
        this.Configuration.LazyLoadingEnabled = false;
    }

    /* Used for Unit Testing with **SQL CE** 
    public DbContextModel(string connectionString): base("TESTMODEL")//connectionString)
    {
        this.Configuration.LazyLoadingEnabled = true;
    }

    public DbContextModel(SqlCeConnectionFactory s)
    {
        this.Configuration.LazyLoadingEnabled = false;

    }

    */


}