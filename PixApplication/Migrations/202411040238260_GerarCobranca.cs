namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GerarCobranca : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CobrancaPix",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCobranca = c.String(nullable: false),
                        LinkPagamento = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Identificacao = c.String(nullable: false, maxLength: 14),
                        NomeDevedor = c.String(nullable: false, maxLength: 50),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CobrancaPix");
        }
    }
}
