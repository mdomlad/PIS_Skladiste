namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Skladiste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanSkladistenjas", "ProizvodID", "dbo.Proizvods");
            DropIndex("dbo.PlanSkladistenjas", new[] { "ProizvodID" });
            AddColumn("dbo.SkladisteLokacijas", "ProizvodID", c => c.Int(nullable: false));
            AlterColumn("dbo.PlanSkladistenjas", "DatumDo", c => c.DateTime());
            CreateIndex("dbo.SkladisteLokacijas", "ProizvodID");
            AddForeignKey("dbo.SkladisteLokacijas", "ProizvodID", "dbo.Proizvods", "ID", cascadeDelete: true);
            DropColumn("dbo.PlanSkladistenjas", "ProizvodID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanSkladistenjas", "ProizvodID", c => c.Int(nullable: false));
            DropForeignKey("dbo.SkladisteLokacijas", "ProizvodID", "dbo.Proizvods");
            DropIndex("dbo.SkladisteLokacijas", new[] { "ProizvodID" });
            AlterColumn("dbo.PlanSkladistenjas", "DatumDo", c => c.DateTime(nullable: false));
            DropColumn("dbo.SkladisteLokacijas", "ProizvodID");
            CreateIndex("dbo.PlanSkladistenjas", "ProizvodID");
            AddForeignKey("dbo.PlanSkladistenjas", "ProizvodID", "dbo.Proizvods", "ID", cascadeDelete: true);
        }
    }
}
