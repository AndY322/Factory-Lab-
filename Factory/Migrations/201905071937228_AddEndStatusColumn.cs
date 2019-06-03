namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndStatusColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetailStatus", "EndStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetailStatus", "EndStatus");
        }
    }
}
