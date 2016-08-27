namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkladisteProizvodFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanSkladistenjas", "ProizvodID", c => c.Int(nullable: false));
            CreateIndex("dbo.PlanSkladistenjas", "ProizvodID");
            AddForeignKey("dbo.PlanSkladistenjas", "ProizvodID", "dbo.Proizvods", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanSkladistenjas", "ProizvodID", "dbo.Proizvods");
            DropIndex("dbo.PlanSkladistenjas", new[] { "ProizvodID" });
            DropColumn("dbo.PlanSkladistenjas", "ProizvodID");
        }
    }
}
