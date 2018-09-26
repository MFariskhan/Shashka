namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartModel2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Carts", new[] { "ProductID" });
            CreateIndex("dbo.Carts", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Carts", new[] { "ProductId" });
            CreateIndex("dbo.Carts", "ProductID");
        }
    }
}
