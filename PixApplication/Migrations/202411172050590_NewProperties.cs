namespace PixApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigPix", "ExpirePix", c => c.String());
            AlterColumn("dbo.ConfigPix", "Name", c => c.String());
            AlterColumn("dbo.ConfigPix", "Identificacao", c => c.String());
            AlterColumn("dbo.ConfigPix", "ChavePix", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConfigPix", "ChavePix", c => c.String(maxLength: 12));
            AlterColumn("dbo.ConfigPix", "Identificacao", c => c.String(maxLength: 12));
            AlterColumn("dbo.ConfigPix", "Name", c => c.String(maxLength: 12));
            DropColumn("dbo.ConfigPix", "ExpirePix");
        }
    }
}
