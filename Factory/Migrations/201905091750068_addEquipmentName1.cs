namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEquipmentName1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "Name");
        }
    }
}
