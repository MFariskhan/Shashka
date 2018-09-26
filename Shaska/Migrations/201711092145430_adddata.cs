namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sizes", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Sizes", "Product_ProductId");
            AddForeignKey("dbo.Sizes", "Product_ProductId", "dbo.Products", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sizes", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Sizes", new[] { "Product_ProductId" });
            DropColumn("dbo.Sizes", "Product_ProductId");
        }
    }
}
