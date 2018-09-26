namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PhoneNumber", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
