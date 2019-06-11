namespace EmployeeTrainingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project_designation_EmployeeProjectDetailstabl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TMS_Designation",
                c => new
                    {
                        DesignationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.DesignationID);
            
            CreateTable(
                "dbo.TMS_ProjectMaster",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClientName = c.String(),
                        StarDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            AddColumn("dbo.TMS_UserMaster", "MobNo", c => c.String());
            AddColumn("dbo.TMS_UserMaster", "PAN", c => c.String());
            AddColumn("dbo.TMS_UserMaster", "Address", c => c.String());
            AddColumn("dbo.TMS_UserMaster", "Experience", c => c.Int(nullable: false));
            AddColumn("dbo.TMS_UserMaster", "PassportNumber", c => c.String());
            AddColumn("dbo.TMS_UserMaster", "PassportExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TMS_UserMaster", "DesignationID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TMS_UserMaster", "DesignationID");
            DropColumn("dbo.TMS_UserMaster", "PassportExpiryDate");
            DropColumn("dbo.TMS_UserMaster", "PassportNumber");
            DropColumn("dbo.TMS_UserMaster", "Experience");
            DropColumn("dbo.TMS_UserMaster", "Address");
            DropColumn("dbo.TMS_UserMaster", "PAN");
            DropColumn("dbo.TMS_UserMaster", "MobNo");
            DropTable("dbo.TMS_ProjectMaster");
            DropTable("dbo.TMS_Designation");
        }
    }
}
