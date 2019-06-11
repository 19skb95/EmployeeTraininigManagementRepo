using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTrainingManagement.Model
{
    public class TMS_ProjectMaster
    {
        [Key]
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}