namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderDataModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Key", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Key");
        }
    }
}
