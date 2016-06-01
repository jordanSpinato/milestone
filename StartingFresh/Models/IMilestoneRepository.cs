using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartingFresh.Models {
    public interface IMilestoneRepository
    {
        IQueryable<MilestoneModel> Milestones { get; }
        MilestoneModel Save(MilestoneModel milestone);
        void Delete(MilestoneModel milestone);

    }
    
}
