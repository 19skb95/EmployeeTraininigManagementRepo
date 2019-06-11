using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EmployeeTrainingManagement.Model
{
    public class TMS_UserMaster
    {
       
        [Key]
        public int UserId { get; set; }
       public string UserMail { get; set; }   
       public string Password { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public System.DateTime DOJ { get; set; }
        public System.DateTime DOB { get; set; }
        public Nullable<int> ManagerID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Notification { get; set; }
        public Nullable<int> PrimarySkillId { get; set; }
        public string SkillId { get; set; }
        public string MobNo { get; set; }
        public string PAN { get; set; }
        public string Address { get; set; }
        public int Experience { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public int DesignationID { get; set; }

        public virtual TMS_RoleMaster TMS_RoleMaster { get; set; }
       // public virtual TMS_Designation TMS_Designation { get; set; }
        //public virtual TMS_SkillMaster TMS_SkillMaster { get; set; }
        
    }
}