namespace Archived.ClientImport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class org : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company_Number = c.String(nullable: false, maxLength: 30),
                        Level = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Org_Number = c.String(),
                        Tier_Id = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.Company_Number, t.Level });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Organization");
        }
    }
}
