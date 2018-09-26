namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeinproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Images", "Product_ProductId");
            AddForeignKey("dbo.Images", "Product_ProductId", "dbo.Products", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Images", new[] { "Product_ProductId" });
            DropColumn("dbo.Images", "Product_ProductId");
        }
    }
}
