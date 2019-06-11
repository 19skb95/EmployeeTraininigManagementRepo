using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EmployeeTrainingManagement.Model
{
    public class TMS_Request
    {
        [Key]
        public int RequestId { get; set; }
        public int BatchId { get; set; }
        public int UserId { get; set; }
        public int ManagerId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}