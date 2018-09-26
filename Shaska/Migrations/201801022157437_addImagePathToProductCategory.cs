namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImagePathToProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "ImagePath");
        }
    }
}
