using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace AngryBirdsDb
{
    public class Seed
    {

        public Seed(AngryBirdsDb.GameContext context)
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

            GameList gameJ = new GameList()
            {
                GameId = 1,
                PlayerId = 1,
                TrackId = 1,
                GameScore = 8,

            };
            context.GameLists.Add(gameJ);

            GameList gameT = new GameList()
            {
                GameId = 2,
                PlayerId = 2,
                TrackId = 1,
                GameScore = 6,

            };
            context.GameLists.AddOrUpdate(gameT);

        }

    }
}

