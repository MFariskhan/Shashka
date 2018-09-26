namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderModel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Barcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Barcode");
        }
    }
}
