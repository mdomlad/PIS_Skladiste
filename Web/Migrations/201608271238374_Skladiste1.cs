namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Skladiste1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas");
            DropIndex("dbo.PlanSkladistenjas", new[] { "SkladisteLokacijaID" });
            DropTable("dbo.PlanSkladistenjas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlanSkladistenjas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(),
                        SkladisteLokacijaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.PlanSkladistenjas", "SkladisteLokacijaID");
            AddForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas", "ProizvodID", cascadeDelete: true);
        }
    }
}
