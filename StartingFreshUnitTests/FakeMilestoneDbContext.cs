using System;
using System.CodeDom;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FakeDbSet;
using StartingFresh.Models;


namespace StartingFreshUnitTests
{
    public class FakeMilestoneDbContext : DbContextModel, IDisposable
    {
        public Func<Exception> ThrowException { get; set; }

        public FakeMilestoneDbContext(string xu)
        {
            // use our DbSets to be in memory DB sets not SQL server DB sets
            Milestones = new InMemoryDbSet<MilestoneModel> { FindFunction = (a, i) => a.FirstOrDefault(x => x.MilestoneId == i.Cast<int>().First()) };
        }

       // public new IDbSet<MilestoneModel> Milestones { get; set; }



        // the interface does not come with a save changes function so we must create one
        public int SaveChanges()
        {
           
            // pretend that each entity get a database id when we hit save
            if (ThrowException != null)
                throw ThrowException();

            int changes = 0;
            changes += DbSetHelper.IncrementPrimaryKey(x => x.MilestoneId, Milestones);

            return changes;
        }

        public void Dispose()
        {
            // dont do anything for this

        }

    }
}

