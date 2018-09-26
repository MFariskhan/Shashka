namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSizeIDtoSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sizes", "SizeId", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sizes", "SizeId");
        }
    }
}
