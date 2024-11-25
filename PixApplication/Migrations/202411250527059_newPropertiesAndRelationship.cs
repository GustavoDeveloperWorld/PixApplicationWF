namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPropertiesAndRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CobrancaPix", "Id", "dbo.Pedido");
            DropIndex("dbo.CobrancaPix", new[] { "Id" });
            DropPrimaryKey("dbo.CobrancaPix");
            AddColumn("dbo.CobrancaPix", "Pedido_Id", c => c.Int());
            AlterColumn("dbo.CobrancaPix", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CobrancaPix", "Id");
            CreateIndex("dbo.CobrancaPix", "Pedido_Id");
            AddForeignKey("dbo.CobrancaPix", "Pedido_Id", "dbo.Pedido", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CobrancaPix", "Pedido_Id", "dbo.Pedido");
            DropIndex("dbo.CobrancaPix", new[] { "Pedido_Id" });
            DropPrimaryKey("dbo.CobrancaPix");
            AlterColumn("dbo.CobrancaPix", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.CobrancaPix", "Pedido_Id");
            AddPrimaryKey("dbo.CobrancaPix", "Id");
            CreateIndex("dbo.CobrancaPix", "Id");
            AddForeignKey("dbo.CobrancaPix", "Id", "dbo.Pedido", "Id");
        }
    }
}
