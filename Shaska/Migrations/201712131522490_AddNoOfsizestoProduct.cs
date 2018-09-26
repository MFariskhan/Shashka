namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoOfsizestoProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NoOfSizes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NoOfSizes");
        }
    }
}
