namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameColumnCreatedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routings", "StartProduction", c => c.DateTime(nullable: false));
            DropColumn("dbo.Routings", "CreatedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routings", "CreatedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Routings", "StartProduction");
        }
    }
}
