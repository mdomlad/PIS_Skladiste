namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Primkas", "DjelatnikID", "dbo.AspNetUsers");
            DropIndex("dbo.Primkas", new[] { "DjelatnikID" });
            AlterColumn("dbo.Primkas", "DjelatnikID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Primkas", "DjelatnikID");
            AddForeignKey("dbo.Primkas", "DjelatnikID", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Primkas", "DjelatnikID", "dbo.AspNetUsers");
            DropIndex("dbo.Primkas", new[] { "DjelatnikID" });
            AlterColumn("dbo.Primkas", "DjelatnikID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Primkas", "DjelatnikID");
            AddForeignKey("dbo.Primkas", "DjelatnikID", "dbo.AspNetUsers", "Id");
        }
    }
}
