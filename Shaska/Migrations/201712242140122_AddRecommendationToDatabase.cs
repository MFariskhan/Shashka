namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecommendationToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recommendations", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Recommendations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Recommendations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Recommendations", new[] { "ProductId" });
            DropTable("dbo.Recommendations");
        }
    }
}
