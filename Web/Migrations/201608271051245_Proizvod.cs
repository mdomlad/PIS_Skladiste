namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Proizvod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkladisteLokacijas", "ProizvodID", "dbo.Proizvods");
            DropForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas");
            DropPrimaryKey("dbo.SkladisteLokacijas");
            AddPrimaryKey("dbo.SkladisteLokacijas", "ProizvodID");
            AddForeignKey("dbo.SkladisteLokacijas", "ProizvodID", "dbo.Proizvods", "ID");
            AddForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas", "ProizvodID", cascadeDelete: true);
            DropColumn("dbo.SkladisteLokacijas", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SkladisteLokacijas", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas");
            DropForeignKey("dbo.SkladisteLokacijas", "ProizvodID", "dbo.Proizvods");
            DropPrimaryKey("dbo.SkladisteLokacijas");
            AddPrimaryKey("dbo.SkladisteLokacijas", "ID");
            AddForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SkladisteLokacijas", "ProizvodID", "dbo.Proizvods", "ID", cascadeDelete: true);
        }
    }
}
