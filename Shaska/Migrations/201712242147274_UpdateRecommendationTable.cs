namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRecommendationTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Recommendations", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Recommendations", "ApplicationUserId");
            RenameColumn(table: "dbo.Recommendations", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Recommendations", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Recommendations", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Recommendations", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Recommendations", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Recommendations", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Recommendations", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recommendations", "ApplicationUser_Id");
        }
    }
}
