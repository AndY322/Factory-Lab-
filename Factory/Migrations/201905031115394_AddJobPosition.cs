namespace Factory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobPosition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Teams", "PositionId", c => c.Int());
            CreateIndex("dbo.Teams", "PositionId");
            AddForeignKey("dbo.Teams", "PositionId", "dbo.JobPositions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "PositionId", "dbo.JobPositions");
            DropIndex("dbo.Teams", new[] { "PositionId" });
            DropColumn("dbo.Teams", "PositionId");
            DropTable("dbo.JobPositions");
        }
    }
}
