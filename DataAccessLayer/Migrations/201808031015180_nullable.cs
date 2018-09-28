namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "percentageScore", c => c.Int());
            AlterColumn("dbo.Test", "completionTime", c => c.Time(precision: 7));
            AlterColumn("dbo.Test", "expirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "expirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Test", "completionTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Test", "percentageScore", c => c.Int(nullable: false));
        }
    }
}
