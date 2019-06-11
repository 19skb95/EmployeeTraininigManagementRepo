using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTrainingManagement.Model
{
    public class TMS_Designation
    {
        [Key]
        public int DesignationID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}