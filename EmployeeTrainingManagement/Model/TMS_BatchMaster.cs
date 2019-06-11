using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EmployeeTrainingManagement.Model
{
    public class TMS_BatchMaster
    {
        [Key]
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int CourseId { get; set; }
        
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> BatchCount { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}