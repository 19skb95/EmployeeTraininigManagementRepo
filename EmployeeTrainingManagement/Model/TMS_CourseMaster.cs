using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EmployeeTrainingManagement.Model
{
    public class TMS_CourseMaster
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}