namespace AngryBirdsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablesfrodbaddedwithiniatialvaluesinseed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameList",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        GameScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.PlayerList", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.TrackList", t => t.TrackId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TrackId);
            
            CreateTable(
                "dbo.PlayerList",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.TrackList",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        NrBird = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameList", "TrackId", "dbo.TrackList");
            DropForeignKey("dbo.GameList", "PlayerId", "dbo.PlayerList");
            DropIndex("dbo.GameList", new[] { "TrackId" });
            DropIndex("dbo.GameList", new[] { "PlayerId" });
            DropTable("dbo.TrackList");
            DropTable("dbo.PlayerList");
            DropTable("dbo.GameList");
        }
    }
}
