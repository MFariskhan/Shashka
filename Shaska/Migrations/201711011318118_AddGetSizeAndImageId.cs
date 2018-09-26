namespace Shaska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGetSizeAndImageId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GetSizeAndImageIds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SavedSizeAndImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GetSizeAndImageIds");
        }
    }
}
