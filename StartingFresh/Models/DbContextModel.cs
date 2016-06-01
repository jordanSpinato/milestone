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
    public class DbContextModel : DbContext, IDbContext
    {
        public DbContextModel()
          : base(ConfigurationManager.ConnectionStrings["MilestoneModel"].ConnectionString)
        {
           Database.SetInitializer<DbContextModel>(new DropCreateDatabaseIfModelChanges<DbContextModel>());
        }

        public DbContextModel(string nameOrConnectionString)
          : base(ConfigurationManager.ConnectionStrings["MDB"].ConnectionString) 
            {
           Database.SetInitializer<DbContextModel>(new DropCreateDatabaseIfModelChanges<DbContextModel>());
        }


    public IDbSet<MilestoneModel> Milestones { get; set; }    
    }
    
    public interface IDbContext
    {
        IDbSet<MilestoneModel> Milestones { get; set; }

        int SaveChanges();


    }

    public class ValidateDatabase<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext {
        public void InitializeDatabase(TContext context) {
            if (!context.Database.Exists()) {
                throw new ConfigurationErrorsException("Database does not exist");
            }
        }
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