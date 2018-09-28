namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "completionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "completionDate", c => c.DateTime(nullable: false));
        }
    }
}
