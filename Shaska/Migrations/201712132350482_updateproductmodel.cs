namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproductmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "SizeId");
            DropColumn("dbo.Products", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "SizeId", c => c.Int(nullable: false));
        }
    }
}
