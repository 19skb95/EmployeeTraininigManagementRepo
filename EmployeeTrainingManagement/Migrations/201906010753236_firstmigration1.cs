namespace EmployeeTrainingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TMS_BatchMaster",
                c => new
                    {
                        BatchId = c.Int(nullable: false, identity: true),
                        BatchName = c.String(),
                        CourseId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        BatchCount = c.Int(),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.BatchId);
            
            CreateTable(
                "dbo.TMS_CourseMaster",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        Duration = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.TMS_Request",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        Status = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.RequestId);
            
            CreateTable(
                "dbo.TMS_RoleMaster",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.TMS_UserMaster",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserMail = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        RoleId = c.Int(nullable: false),
                        DOJ = c.DateTime(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        ManagerID = c.Int(),
                        CreatedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                        Notification = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.TMS_RoleMaster", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TMS_UserMaster", "RoleId", "dbo.TMS_RoleMaster");
            DropIndex("dbo.TMS_UserMaster", new[] { "RoleId" });
            DropTable("dbo.TMS_UserMaster");
            DropTable("dbo.TMS_RoleMaster");
            DropTable("dbo.TMS_Request");
            DropTable("dbo.TMS_CourseMaster");
            DropTable("dbo.TMS_BatchMaster");
        }
    }
}
