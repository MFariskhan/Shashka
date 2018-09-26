namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductCategoryModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "CategoryDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "CategoryDescription");
        }
    }
}
