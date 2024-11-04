namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authentications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientID = c.String(nullable: false, maxLength: 500),
                        ClientSecret = c.String(nullable: false, maxLength: 500),
                        ApplicationKey = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConfigPix",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12),
                        Identificacao = c.String(nullable: false, maxLength: 12),
                        ChavePix = c.String(nullable: false, maxLength: 12),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConfigPix");
            DropTable("dbo.Authentications");
        }
    }
}
