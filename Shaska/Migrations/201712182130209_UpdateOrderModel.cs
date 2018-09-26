namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderPostDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "OrderConfirmDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderConfirmDate");
            DropColumn("dbo.Orders", "OrderPostDate");
        }
    }
}
