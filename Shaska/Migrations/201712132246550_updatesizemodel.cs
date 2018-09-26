namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesizemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sizes", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Sizes", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.Sizes", name: "Product_ProductId", newName: "ProductId");
            AlterColumn("dbo.Sizes", "ProductId", c => c.Int(nullable: true));
            CreateIndex("dbo.Sizes", "ProductId");
            AddForeignKey("dbo.Sizes", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Sizes", "SizeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sizes", "SizeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sizes", "ProductId", "dbo.Products");
            DropIndex("dbo.Sizes", new[] { "ProductId" });
            AlterColumn("dbo.Sizes", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Sizes", name: "ProductId", newName: "Product_ProductId");
            CreateIndex("dbo.Sizes", "Product_ProductId");
            AddForeignKey("dbo.Sizes", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
