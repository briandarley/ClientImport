namespace Data.EntityInformation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsourcepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ENTITY_CONFIGURATION", "SOURCE_FILE_PATH", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ENTITY_CONFIGURATION", "SOURCE_FILE_PATH");
        }
    }
}
