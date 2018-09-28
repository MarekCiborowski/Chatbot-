namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimemigrate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "expirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "expirationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
