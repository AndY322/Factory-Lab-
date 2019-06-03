namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProcessStepColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetailStatus", "ProcessStep", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetailStatus", "ProcessStep");
        }
    }
}
