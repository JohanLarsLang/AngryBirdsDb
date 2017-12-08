namespace AngryBirdsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablesaddedtoemptydatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        GameScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.Tracks", t => t.TrackId)
                .Index(t => t.PlayerId)
                .Index(t => t.TrackId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        NrBird = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.Games", "PlayerId", "dbo.Players");
            DropIndex("dbo.Games", new[] { "TrackId" });
            DropIndex("dbo.Games", new[] { "PlayerId" });
            DropTable("dbo.Tracks");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
        }
    }
}
