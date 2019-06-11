using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTrainingManagement.Model
{
    public class ViewModelClass
    {
        public class ModelUserDetails
        {
            public string ManagerName { get; set; }
            public string Name { get; set; } 
            public string CurrentProject { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string PrimarySkill { get; set; }
            public DateTime DOJ { get; set; }
            public DateTime DOB { get; set; }
            public string RegisteredEmail { get; set; }
            public string Role { get; set; }
            public string Designation { get; set; }
            public string MobileNo { get; set; }
            public string PAN { get; set; }
            public string Address { get; set; }
            public int Experience { get; set; }
            public string PassportNumber { get; set; }
            public DateTime PassportExpiryDate { get; set; }
            
        }
        
       

        public class ModelSkillDetails
        {
            public string Name { get; set; }
            public DateTime DOJ { get; set; }
            public string PrimarySkill { get; set; }
        }
        public class ModelMyTeam
        {
            public Nullable<int> UserId { get; set; }
            public string UserName { get; set; }
            public string CurrentProject { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string PrimarySkill { get; set; }

        }
        public class ModelUserView
        {
            public Nullable<int> id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string rolename { get; set; }
            public string managername { get; set; }
            public DateTime doj { get; set; }
            public DateTime dob { get; set; }
        }

        public class ModelBatchView
        {
            public int BatchId { get; set; }
            public string BatchName { get; set; }
            public string CourseName { get; set; }
            public int CourseDuration { get; set; }
            public Nullable<System.DateTime> StartDate { get; set; }
            public Nullable<System.DateTime> EndDate { get; set; }
            public Nullable<int> BatchCount { get; set; }

        }

        public class ViewRequestModel
        {
            public int RequestId { get; set; }
            public string Batchname { get; set; }
            public string CourseName { get; set; }
            public string RequetsedBy { get; set; }
            public string RequetsedTo { get; set; }
            public string Status { get; set; }
            public int CourseDuration { get; set; }
            public Nullable<System.DateTime> StartDate { get; set; }
            public Nullable<System.DateTime> EndDate { get; set; }
            public Nullable<int> BatchCount { get; set; }



        }
    }
}