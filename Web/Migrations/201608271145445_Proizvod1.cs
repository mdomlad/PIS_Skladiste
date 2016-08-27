namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Proizvod1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaIzdatnices", "JedinicaMjereID", "dbo.JedinicaMjeres");
            DropForeignKey("dbo.StavkaPrimkes", "JedinicaMjereID", "dbo.JedinicaMjeres");
            DropIndex("dbo.StavkaIzdatnices", new[] { "JedinicaMjereID" });
            DropIndex("dbo.StavkaPrimkes", new[] { "JedinicaMjereID" });
            AddColumn("dbo.SkladisteLokacijas", "JedinicaMjereID", c => c.Int(nullable: false));
            CreateIndex("dbo.SkladisteLokacijas", "JedinicaMjereID");
            AddForeignKey("dbo.SkladisteLokacijas", "JedinicaMjereID", "dbo.JedinicaMjeres", "ID", cascadeDelete: true);
            DropColumn("dbo.StavkaIzdatnices", "JedinicaMjereID");
            DropColumn("dbo.StavkaPrimkes", "JedinicaMjereID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StavkaPrimkes", "JedinicaMjereID", c => c.Int(nullable: false));
            AddColumn("dbo.StavkaIzdatnices", "JedinicaMjereID", c => c.Int(nullable: false));
            DropForeignKey("dbo.SkladisteLokacijas", "JedinicaMjereID", "dbo.JedinicaMjeres");
            DropIndex("dbo.SkladisteLokacijas", new[] { "JedinicaMjereID" });
            DropColumn("dbo.SkladisteLokacijas", "JedinicaMjereID");
            CreateIndex("dbo.StavkaPrimkes", "JedinicaMjereID");
            CreateIndex("dbo.StavkaIzdatnices", "JedinicaMjereID");
            AddForeignKey("dbo.StavkaPrimkes", "JedinicaMjereID", "dbo.JedinicaMjeres", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StavkaIzdatnices", "JedinicaMjereID", "dbo.JedinicaMjeres", "ID", cascadeDelete: true);
        }
    }
}
