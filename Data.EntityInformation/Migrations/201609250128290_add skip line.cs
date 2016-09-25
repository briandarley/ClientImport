namespace Data.EntityInformation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addskipline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ENTITY_CONFIGURATION", "SKIP_FIRST_LINE", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ENTITY_CONFIGURATION", "SKIP_FIRST_LINE");
        }
    }
}
