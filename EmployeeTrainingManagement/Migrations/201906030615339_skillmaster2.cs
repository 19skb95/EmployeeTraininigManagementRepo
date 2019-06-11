namespace EmployeeTrainingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skillmaster2 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TMS_SkillMaster");
        }
    }
}
