namespace EmployeeTrainingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skillmaster : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TMS_SkillMaster",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            AddColumn("dbo.TMS_UserMaster", "PrimarySkillId", c => c.Int());
            AddColumn("dbo.TMS_UserMaster", "SkillId", c => c.String());
            AddColumn("dbo.TMS_UserMaster", "TMS_SkillMaster_SkillId", c => c.Int());
            CreateIndex("dbo.TMS_UserMaster", "TMS_SkillMaster_SkillId");
            AddForeignKey("dbo.TMS_UserMaster", "TMS_SkillMaster_SkillId", "dbo.TMS_SkillMaster", "SkillId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TMS_UserMaster", "TMS_SkillMaster_SkillId", "dbo.TMS_SkillMaster");
            DropIndex("dbo.TMS_UserMaster", new[] { "TMS_SkillMaster_SkillId" });
            DropColumn("dbo.TMS_UserMaster", "TMS_SkillMaster_SkillId");
            DropColumn("dbo.TMS_UserMaster", "SkillId");
            DropColumn("dbo.TMS_UserMaster", "PrimarySkillId");
            DropTable("dbo.TMS_SkillMaster");
        }
    }
}
