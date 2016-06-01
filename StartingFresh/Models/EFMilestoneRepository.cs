using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using StartingFresh.Models;

namespace StartingFresh.Models
{
    public class EfMilestoneRepository : IMilestoneRepository
    {
        DbContextModel context = new DbContextModel();

        public IQueryable<MilestoneModel> Milestones {
            get { return context.Milestones; }

        }

        public MilestoneModel Save(MilestoneModel model)
        {
            if (model.MilestoneId == 0)
            {
                context.Milestones.Add(model);
            }

            else
            {
                context.Entry(model).State = EntityState.Modified;
            }

            context.SaveChanges();
            return model;
        }

        public void Delete (MilestoneModel model)
        {
            context.Milestones.Remove(model);
            context.SaveChanges();
        }



    }

}
