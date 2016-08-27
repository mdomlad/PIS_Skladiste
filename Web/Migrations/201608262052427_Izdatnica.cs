namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Izdatnica : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Izdatnicas", "DjelatnikID", "dbo.AspNetUsers");
            DropIndex("dbo.Izdatnicas", new[] { "DjelatnikID" });
            AlterColumn("dbo.Izdatnicas", "DjelatnikID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Izdatnicas", "DjelatnikID");
            AddForeignKey("dbo.Izdatnicas", "DjelatnikID", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Izdatnicas", "DjelatnikID", "dbo.AspNetUsers");
            DropIndex("dbo.Izdatnicas", new[] { "DjelatnikID" });
            AlterColumn("dbo.Izdatnicas", "DjelatnikID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Izdatnicas", "DjelatnikID");
            AddForeignKey("dbo.Izdatnicas", "DjelatnikID", "dbo.AspNetUsers", "Id");
        }
    }
}
