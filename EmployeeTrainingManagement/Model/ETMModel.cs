namespace EmployeeTrainingManagement.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ETMModel : DbContext
    {
        public ETMModel()
            : base("name=ETMModel")
        {
        }
        public DbSet<TMS_BatchMaster> TMS_BatchMaster { get; set; }
        public DbSet<TMS_CourseMaster> TMS_CourseMaster { get; set; }
        public DbSet<TMS_Request> TMS_Request { get; set; }
        public DbSet<TMS_RoleMaster> TMS_RoleMaster { get; set; }
        public DbSet<TMS_UserMaster> TMS_UserMaster { get; set; }
        public DbSet<TMS_SkillMaster> tMS_SkillMaster { get; set; }
        public DbSet<TMS_Designation> TMS_Designations { get; set; }
        public DbSet<TMS_ProjectMaster> TMS_ProjectMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
