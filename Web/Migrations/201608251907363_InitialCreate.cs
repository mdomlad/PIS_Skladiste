namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Ime = c.String(maxLength: 50),
                        Prezime = c.String(maxLength: 50),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Izdatnicas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumIzdavanja = c.DateTime(nullable: false),
                        DjelatnikID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.DjelatnikID)
                .Index(t => t.DjelatnikID);
            
            CreateTable(
                "dbo.Proizvods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                        Cijena = c.Double(nullable: false),
                        Izdatnica_ID = c.Int(),
                        Primka_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Izdatnicas", t => t.Izdatnica_ID)
                .ForeignKey("dbo.Primkas", t => t.Primka_ID)
                .Index(t => t.Izdatnica_ID)
                .Index(t => t.Primka_ID);
            
            CreateTable(
                "dbo.StavkaIzdatnices",
                c => new
                    {
                        IzdatnicaID = c.Int(nullable: false),
                        ProizvodID = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        JedinicaMjereID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IzdatnicaID, t.ProizvodID })
                .ForeignKey("dbo.Izdatnicas", t => t.IzdatnicaID, cascadeDelete: true)
                .ForeignKey("dbo.JedinicaMjeres", t => t.JedinicaMjereID, cascadeDelete: true)
                .ForeignKey("dbo.Proizvods", t => t.ProizvodID, cascadeDelete: true)
                .ForeignKey("dbo.StatusDokumentas", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.IzdatnicaID)
                .Index(t => t.ProizvodID)
                .Index(t => t.StatusID)
                .Index(t => t.JedinicaMjereID);
            
            CreateTable(
                "dbo.JedinicaMjeres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StatusDokumentas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Primkas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DjelatnikID = c.String(maxLength: 128),
                        DatumZaprimanja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.DjelatnikID)
                .Index(t => t.DjelatnikID);
            
            CreateTable(
                "dbo.StavkaPrimkes",
                c => new
                    {
                        PrimkaID = c.Int(nullable: false),
                        ProizvodID = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        JedinicaMjereID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PrimkaID, t.ProizvodID })
                .ForeignKey("dbo.JedinicaMjeres", t => t.JedinicaMjereID, cascadeDelete: true)
                .ForeignKey("dbo.Primkas", t => t.PrimkaID, cascadeDelete: true)
                .ForeignKey("dbo.Proizvods", t => t.ProizvodID, cascadeDelete: true)
                .ForeignKey("dbo.StatusDokumentas", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.PrimkaID)
                .Index(t => t.ProizvodID)
                .Index(t => t.StatusID)
                .Index(t => t.JedinicaMjereID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PlanSkladistenjas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                        SkladisteLokacijaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SkladisteLokacijas", t => t.SkladisteLokacijaID, cascadeDelete: true)
                .Index(t => t.SkladisteLokacijaID);
            
            CreateTable(
                "dbo.SkladisteLokacijas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MinimalnaKolicina = c.Double(nullable: false),
                        MaksimalnaKolicina = c.Double(nullable: false),
                        Stanje = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PlanSkladistenjas", "SkladisteLokacijaID", "dbo.SkladisteLokacijas");
            DropForeignKey("dbo.StavkaPrimkes", "StatusID", "dbo.StatusDokumentas");
            DropForeignKey("dbo.StavkaPrimkes", "ProizvodID", "dbo.Proizvods");
            DropForeignKey("dbo.StavkaPrimkes", "PrimkaID", "dbo.Primkas");
            DropForeignKey("dbo.StavkaPrimkes", "JedinicaMjereID", "dbo.JedinicaMjeres");
            DropForeignKey("dbo.Proizvods", "Primka_ID", "dbo.Primkas");
            DropForeignKey("dbo.Primkas", "DjelatnikID", "dbo.AspNetUsers");
            DropForeignKey("dbo.StavkaIzdatnices", "StatusID", "dbo.StatusDokumentas");
            DropForeignKey("dbo.StavkaIzdatnices", "ProizvodID", "dbo.Proizvods");
            DropForeignKey("dbo.StavkaIzdatnices", "JedinicaMjereID", "dbo.JedinicaMjeres");
            DropForeignKey("dbo.StavkaIzdatnices", "IzdatnicaID", "dbo.Izdatnicas");
            DropForeignKey("dbo.Proizvods", "Izdatnica_ID", "dbo.Izdatnicas");
            DropForeignKey("dbo.Izdatnicas", "DjelatnikID", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PlanSkladistenjas", new[] { "SkladisteLokacijaID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.StavkaPrimkes", new[] { "JedinicaMjereID" });
            DropIndex("dbo.StavkaPrimkes", new[] { "StatusID" });
            DropIndex("dbo.StavkaPrimkes", new[] { "ProizvodID" });
            DropIndex("dbo.StavkaPrimkes", new[] { "PrimkaID" });
            DropIndex("dbo.Primkas", new[] { "DjelatnikID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.StavkaIzdatnices", new[] { "JedinicaMjereID" });
            DropIndex("dbo.StavkaIzdatnices", new[] { "StatusID" });
            DropIndex("dbo.StavkaIzdatnices", new[] { "ProizvodID" });
            DropIndex("dbo.StavkaIzdatnices", new[] { "IzdatnicaID" });
            DropIndex("dbo.Proizvods", new[] { "Primka_ID" });
            DropIndex("dbo.Proizvods", new[] { "Izdatnica_ID" });
            DropIndex("dbo.Izdatnicas", new[] { "DjelatnikID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SkladisteLokacijas");
            DropTable("dbo.PlanSkladistenjas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.StavkaPrimkes");
            DropTable("dbo.Primkas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.StatusDokumentas");
            DropTable("dbo.JedinicaMjeres");
            DropTable("dbo.StavkaIzdatnices");
            DropTable("dbo.Proizvods");
            DropTable("dbo.Izdatnicas");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
