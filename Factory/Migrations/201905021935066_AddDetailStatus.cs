namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetailStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Routings", "StatusId", c => c.Int());
            CreateIndex("dbo.Routings", "StatusId");
            AddForeignKey("dbo.Routings", "StatusId", "dbo.DetailStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routings", "StatusId", "dbo.DetailStatus");
            DropIndex("dbo.Routings", new[] { "StatusId" });
            DropColumn("dbo.Routings", "StatusId");
            DropTable("dbo.DetailStatus");
        }
    }
}
