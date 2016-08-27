namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalculatingStanje : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SkladisteLokacijas", "Stanje", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SkladisteLokacijas", "Stanje", c => c.Int(nullable: false));
        }
    }
}
