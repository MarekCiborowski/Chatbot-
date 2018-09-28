namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GivenAnswer", "points", c => c.Int(nullable: false));
            AddColumn("dbo.TestTemplate", "description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestTemplate", "description");
            DropColumn("dbo.GivenAnswer", "points");
        }
    }
}
