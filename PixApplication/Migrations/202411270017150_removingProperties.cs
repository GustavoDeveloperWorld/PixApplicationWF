namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Insumo", "Pedido_Id", "dbo.Pedido");
            DropIndex("dbo.Insumo", new[] { "Pedido_Id" });
            DropColumn("dbo.Insumo", "Pedido_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Insumo", "Pedido_Id", c => c.Int());
            CreateIndex("dbo.Insumo", "Pedido_Id");
            AddForeignKey("dbo.Insumo", "Pedido_Id", "dbo.Pedido", "Id");
        }
    }
}
