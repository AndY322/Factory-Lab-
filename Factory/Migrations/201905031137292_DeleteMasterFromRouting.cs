namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMasterFromRouting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routings", "MasterId", "dbo.Employees");
            DropIndex("dbo.Routings", new[] { "MasterId" });
            DropColumn("dbo.Routings", "MasterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routings", "MasterId", c => c.Int());
            CreateIndex("dbo.Routings", "MasterId");
            AddForeignKey("dbo.Routings", "MasterId", "dbo.Employees", "Id");
        }
    }
}
