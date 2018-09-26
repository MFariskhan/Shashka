namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderConfirmById", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "OrderAssignToId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "OrderConfirmById");
            CreateIndex("dbo.Orders", "OrderAssignToId");
            AddForeignKey("dbo.Orders", "OrderAssignToId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Orders", "OrderConfirmById", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderConfirmById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "OrderAssignToId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "OrderAssignToId" });
            DropIndex("dbo.Orders", new[] { "OrderConfirmById" });
            DropColumn("dbo.Orders", "OrderAssignToId");
            DropColumn("dbo.Orders", "OrderConfirmById");
        }
    }
}
