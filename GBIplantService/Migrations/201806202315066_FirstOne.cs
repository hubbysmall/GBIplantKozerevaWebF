namespace GBIplantService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zakazs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        GBIpieceofArtId = c.Int(nullable: false),
                        ExecutorId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateExecute = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Executors", t => t.ExecutorId)
                .ForeignKey("dbo.GBIpieceOfArts", t => t.GBIpieceofArtId, cascadeDelete: true)
                .Index(t => t.BuyerId)
                .Index(t => t.GBIpieceofArtId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GBIpieceOfArts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GBIpieceOfArtNAme = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GBIpieceofArt__ingridient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GBIpieceOfArtId = c.Int(nullable: false),
                        GBIindgridientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GBIindgridients", t => t.GBIindgridientId, cascadeDelete: true)
                .ForeignKey("dbo.GBIpieceOfArts", t => t.GBIpieceOfArtId, cascadeDelete: true)
                .Index(t => t.GBIpieceOfArtId)
                .Index(t => t.GBIindgridientId);
            
            CreateTable(
                "dbo.GBIindgridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GBIindgridientName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storage__GBIingridient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        GBIingridientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GBIindgridients", t => t.GBIingridientId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.GBIingridientId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zakazs", "GBIpieceofArtId", "dbo.GBIpieceOfArts");
            DropForeignKey("dbo.GBIpieceofArt__ingridient", "GBIpieceOfArtId", "dbo.GBIpieceOfArts");
            DropForeignKey("dbo.Storage__GBIingridient", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.Storage__GBIingridient", "GBIingridientId", "dbo.GBIindgridients");
            DropForeignKey("dbo.GBIpieceofArt__ingridient", "GBIindgridientId", "dbo.GBIindgridients");
            DropForeignKey("dbo.Zakazs", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.Zakazs", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.Storage__GBIingridient", new[] { "GBIingridientId" });
            DropIndex("dbo.Storage__GBIingridient", new[] { "StorageId" });
            DropIndex("dbo.GBIpieceofArt__ingridient", new[] { "GBIindgridientId" });
            DropIndex("dbo.GBIpieceofArt__ingridient", new[] { "GBIpieceOfArtId" });
            DropIndex("dbo.Zakazs", new[] { "ExecutorId" });
            DropIndex("dbo.Zakazs", new[] { "GBIpieceofArtId" });
            DropIndex("dbo.Zakazs", new[] { "BuyerId" });
            DropTable("dbo.Storages");
            DropTable("dbo.Storage__GBIingridient");
            DropTable("dbo.GBIindgridients");
            DropTable("dbo.GBIpieceofArt__ingridient");
            DropTable("dbo.GBIpieceOfArts");
            DropTable("dbo.Executors");
            DropTable("dbo.Zakazs");
            DropTable("dbo.Buyers");
        }
    }
}
