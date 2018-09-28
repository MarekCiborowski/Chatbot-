namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsReviewedPropertyAddedInTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Test", "isReviewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Test", "isReviewed");
        }
    }
}
