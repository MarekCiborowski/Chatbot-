namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetime2Migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "expirationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "expirationDate", c => c.DateTime());
        }
    }
}
