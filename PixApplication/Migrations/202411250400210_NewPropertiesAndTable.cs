namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertiesAndTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroPedido = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormaPagamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Insumo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        PedidoId = c.Int(nullable: false),
                        Pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedido", t => t.Pedido_Id)
                .ForeignKey("dbo.Pedido", t => t.PedidoId)
                .Index(t => t.PedidoId)
                .Index(t => t.Pedido_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Insumo", "PedidoId", "dbo.Pedido");
            DropForeignKey("dbo.Insumo", "Pedido_Id", "dbo.Pedido");
            DropIndex("dbo.Insumo", new[] { "Pedido_Id" });
            DropIndex("dbo.Insumo", new[] { "PedidoId" });
            DropTable("dbo.Insumo");
            DropTable("dbo.Pedido");
        }
    }
}
