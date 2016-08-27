namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Skladiste2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proizvods", "Naziv", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proizvods", "Naziv", c => c.String());
        }
    }
}
