namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KolicinaToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StavkaIzdatnices", "Kolicina", c => c.Double(nullable: false));
            AlterColumn("dbo.StavkaPrimkes", "Kolicina", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StavkaPrimkes", "Kolicina", c => c.Int(nullable: false));
            AlterColumn("dbo.StavkaIzdatnices", "Kolicina", c => c.Int(nullable: false));
        }
    }
}
