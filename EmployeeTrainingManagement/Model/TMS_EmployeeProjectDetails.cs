using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTrainingManagement.Model
{
    public class TMS_EmployeeProjectDetails
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CurrentProjectID { get; set; }
        public int NextAssignmentID { get; set; }

        


    }
}