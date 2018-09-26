namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateimagemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Images", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.Images", name: "Product_ProductId", newName: "ProductId");
            AlterColumn("dbo.Images", "ProductId", c => c.Int(nullable: true));
            CreateIndex("dbo.Images", "ProductId");
            AddForeignKey("dbo.Images", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Images", "ImageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Images", "ProductId", "dbo.Products");
            DropIndex("dbo.Images", new[] { "ProductId" });
            AlterColumn("dbo.Images", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Images", name: "ProductId", newName: "Product_ProductId");
            CreateIndex("dbo.Images", "Product_ProductId");
            AddForeignKey("dbo.Images", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
