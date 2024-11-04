namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GerarPix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authentications", "ClientID", c => c.String(maxLength: 500));
            AlterColumn("dbo.Authentications", "ClientSecret", c => c.String(maxLength: 500));
            AlterColumn("dbo.Authentications", "ApplicationKey", c => c.String(maxLength: 500));
            AlterColumn("dbo.CobrancaPix", "IdCobranca", c => c.String());
            AlterColumn("dbo.CobrancaPix", "LinkPagamento", c => c.String());
            AlterColumn("dbo.CobrancaPix", "Identificacao", c => c.String(maxLength: 14));
            AlterColumn("dbo.CobrancaPix", "NomeDevedor", c => c.String(maxLength: 50));
            AlterColumn("dbo.CobrancaPix", "DataCriacao", c => c.DateTime());
            AlterColumn("dbo.ConfigPix", "Name", c => c.String(maxLength: 12));
            AlterColumn("dbo.ConfigPix", "Identificacao", c => c.String(maxLength: 12));
            AlterColumn("dbo.ConfigPix", "ChavePix", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConfigPix", "ChavePix", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.ConfigPix", "Identificacao", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.ConfigPix", "Name", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.CobrancaPix", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CobrancaPix", "NomeDevedor", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CobrancaPix", "Identificacao", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("dbo.CobrancaPix", "LinkPagamento", c => c.String(nullable: false));
            AlterColumn("dbo.CobrancaPix", "IdCobranca", c => c.String(nullable: false));
            AlterColumn("dbo.Authentications", "ApplicationKey", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Authentications", "ClientSecret", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Authentications", "ClientID", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
