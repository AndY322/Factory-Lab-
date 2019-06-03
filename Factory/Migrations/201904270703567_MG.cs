namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MG : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routings", "TestField", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "Name");
            DropColumn("dbo.Routings", "TestField");
        }
    }
}
