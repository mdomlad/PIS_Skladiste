namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusDokuenta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaIzdatnices", "StatusID", "dbo.StatusDokumentas");
            DropForeignKey("dbo.StavkaPrimkes", "StatusID", "dbo.StatusDokumentas");
            DropIndex("dbo.StavkaIzdatnices", new[] { "StatusID" });
            DropIndex("dbo.StavkaPrimkes", new[] { "StatusID" });
            AddColumn("dbo.Izdatnicas", "StatusID", c => c.Int(nullable: false));
            AddColumn("dbo.Primkas", "StatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.Izdatnicas", "StatusID");
            CreateIndex("dbo.Primkas", "StatusID");
            AddForeignKey("dbo.Izdatnicas", "StatusID", "dbo.StatusDokumentas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Primkas", "StatusID", "dbo.StatusDokumentas", "ID", cascadeDelete: true);
            DropColumn("dbo.StavkaIzdatnices", "StatusID");
            DropColumn("dbo.StavkaPrimkes", "StatusID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StavkaPrimkes", "StatusID", c => c.Int(nullable: false));
            AddColumn("dbo.StavkaIzdatnices", "StatusID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Primkas", "StatusID", "dbo.StatusDokumentas");
            DropForeignKey("dbo.Izdatnicas", "StatusID", "dbo.StatusDokumentas");
            DropIndex("dbo.Primkas", new[] { "StatusID" });
            DropIndex("dbo.Izdatnicas", new[] { "StatusID" });
            DropColumn("dbo.Primkas", "StatusID");
            DropColumn("dbo.Izdatnicas", "StatusID");
            CreateIndex("dbo.StavkaPrimkes", "StatusID");
            CreateIndex("dbo.StavkaIzdatnices", "StatusID");
            AddForeignKey("dbo.StavkaPrimkes", "StatusID", "dbo.StatusDokumentas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StavkaIzdatnices", "StatusID", "dbo.StatusDokumentas", "ID", cascadeDelete: true);
        }
    }
}
