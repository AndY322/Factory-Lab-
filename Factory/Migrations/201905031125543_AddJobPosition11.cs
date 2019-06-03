namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobPosition11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teams", "PeopleCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "PeopleCount", c => c.Int(nullable: false));
        }
    }
}
