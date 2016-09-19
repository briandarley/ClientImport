namespace Data.EntityInformation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ENTITY_CONFIGURATION",
                c => new
                    {
                        ENTITY_CONFIGURATION_ID = c.Int(nullable: false, identity: true),
                        ENTITY_CODE = c.String(nullable: false),
                        FILE_EXTENSION = c.String(nullable: false),
                        COMPANY_NUMBER = c.String(nullable: false),
                        ENABLED = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ENTITY_CONFIGURATION_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ENTITY_CONFIGURATION");
        }
    }
}
