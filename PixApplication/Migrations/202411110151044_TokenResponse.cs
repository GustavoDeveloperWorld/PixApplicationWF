namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenResponse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        access_token = c.String(),
                        token_type = c.String(),
                        expires_in = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Token");
        }
    }
}
