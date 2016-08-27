namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusDokuentaLabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusDokumentas", "Label", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusDokumentas", "Label");
        }
    }
}
