namespace AngryBirdsDb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AngryBirdsDb.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AngryBirdsDb.GameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngryBirdsDb.GameContext context)
        {
            Player johan = new Player()
            {
                PlayerId = 1,
                PlayerName = "Johan",

            };
            context.Players.Add(johan);

            Player tommy = new Player()
            {
                PlayerId = 2,
                PlayerName = "Cyberpunx",

            };
            context.Players.Add(tommy);

            Track track = new Track()
            {
                TrackId = 1,
                NrBird = 10,

            };
            context.Tracks.AddOrUpdate(track);

            Game gameJ = new Game()
            {
                GameId = 1,
                PlayerId = 1,
                TrackId = 1,
                GameScore = 8,

            };
            context.Games.Add(gameJ);

            Game gameT = new Game()
            {
                GameId = 2,
                PlayerId = 2,
                TrackId = 1,
                GameScore = 6,

            };
            context.Games.AddOrUpdate(gameT);

        
    }
    }
}
