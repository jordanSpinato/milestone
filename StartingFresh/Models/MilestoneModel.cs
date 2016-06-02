/*
 * Author: jordan@enteracloud.com
 * */

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StartingFresh.Models
{
    using System;
    using System.Collections.Generic;
    
    public class MilestoneModel
    {
        [Key]
        [DisplayName("ID")]
        public int MilestoneId { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter an End Date")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public string EndDateString { get; set; }

        public DateTime StartTime { get; set; }
        public string StartTimeString { get; set; }

        public int TotalProjectDays { get; set; }

        public IEnumerable<MilestoneModel> Milestones { get; set; }

    }
}
