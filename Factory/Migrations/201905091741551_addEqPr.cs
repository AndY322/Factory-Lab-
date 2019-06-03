namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEqPr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Processes", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.Processes", new[] { "EquipmentId" });
            AlterColumn("dbo.Processes", "EquipmentId", c => c.Int());
            CreateIndex("dbo.Processes", "EquipmentId");
            AddForeignKey("dbo.Processes", "EquipmentId", "dbo.Equipments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Processes", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.Processes", new[] { "EquipmentId" });
            AlterColumn("dbo.Processes", "EquipmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Processes", "EquipmentId");
            AddForeignKey("dbo.Processes", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: true);
        }
    }
}
