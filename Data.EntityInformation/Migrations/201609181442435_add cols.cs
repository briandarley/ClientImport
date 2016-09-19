namespace Data.EntityInformation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcols : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ENTITY_CONFIGURATION", "ENTITY_CODE", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ENTITY_CONFIGURATION", "FILE_EXTENSION", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.ENTITY_CONFIGURATION", "COMPANY_NUMBER", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ENTITY_CONFIGURATION", "COMPANY_NUMBER", c => c.String(nullable: false));
            AlterColumn("dbo.ENTITY_CONFIGURATION", "FILE_EXTENSION", c => c.String(nullable: false));
            AlterColumn("dbo.ENTITY_CONFIGURATION", "ENTITY_CODE", c => c.String(nullable: false));
        }
    }
}
