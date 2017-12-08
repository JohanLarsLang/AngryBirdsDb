using AngryBirdsDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace AngryBirdsDb
{
    class Program
    {
        static void Main(string[] args)
        {
            bool displayMenu = true;

            while (displayMenu)
            {
                displayMenu = ListPlayersMenu();
            }
        }

        private static bool ListPlayersMenu()
        {
            Console.Clear();
            Console.WriteLine($"****** Angry Bird Database *******\n");
            Console.WriteLine($"All players in the database:\n");

            using (var context = new GameContext())
            {

                var queryPlayers = (from x in context.Players
                                    select x);


                foreach (var x in queryPlayers)
                {
                    Console.WriteLine($"[{x.PlayerId}]: {x.PlayerName}");
                }

                AddPlayer();
            }

            return true;
        }

        private static bool AddPlayer()
        {
            Console.WriteLine();
            Console.WriteLine($"\nEnter existing player name or enter a new player name:");

            string playerName = Console.ReadLine();
            playerName = playerName.ToLower();
            using (var context = new GameContext())
            {

                var queryPlayers = (from x in context.Players
                                    select x);


                if (playerName == "q")
                {
                    return false;
                }

                else
                {
                    bool foundName = false;

                    foreach (var x in queryPlayers)
                    {
                        if (playerName == x.PlayerName)
                        {
                            foundName = true;
                            Console.Clear();
                            ListTracks(playerName);
                        }
                    }

                    if (foundName == false)
                        CreatePlayer(playerName);

                    return true;
                }
            }
        }

        private static void CreatePlayer(string playerName)
        {
            using (var context = new GameContext())
            {
                int nextPlayerId = GetNextPlayerId();

                context.Players.Add(new Player()
                {
                    PlayerId = nextPlayerId,
                    PlayerName = playerName
                });

                context.SaveChanges();
            }
        }

        private static void ListTracks(string playerName)
        {
            using (var context = new GameContext())
            {
                int playerId = GetPlayerId(playerName);

                var queryTracks = (from x in context.Games
                                   where x.PlayerId == playerId
                                   select x);

                Console.WriteLine($"Player: {playerName}\n");

                foreach (var x in queryTracks)
                {
                    int[] scoreNrLeft = GetScore(playerId, x.TrackId);
                    int score = scoreNrLeft[0];
                    int nrLeft = scoreNrLeft[1];

                    List<object> nameHighScore = GetHighScore(x.TrackId);
                    string playerNameHighScore = (string)nameHighScore[0];
                    int trackHighScore = (int)nameHighScore[1];

                    Console.WriteLine($"Track {x.TrackId}: {score} birds ({nrLeft} left) >> Highscore {playerNameHighScore}: {trackHighScore} birds");
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("Enter Track number to change score:");
            Console.WriteLine("Enter t to add new track:");
            Console.WriteLine("Enter q to quit and go back to the players list");

            string trackIdStr = Console.ReadLine();
            trackIdStr = trackIdStr.ToLower();

            if (trackIdStr == "q")
            {
                ListPlayersMenu();
            }

            else if (trackIdStr == "t")
            {
                AddTrack(playerName);
            }

            else
            {
                try
                {
                    int inputTrackId = int.Parse(trackIdStr);

                    int playerId = GetPlayerId(playerName);

                    int currentTrackId = GetCurrentTrackId(playerId);

                    if (inputTrackId >= 1 && inputTrackId <= currentTrackId)
                    {
                        int nrBirds = GetNrBirds(inputTrackId);
                        bool loop = false;
                        int newScore = 20;

                        while (loop != true)
                        {
                            Console.WriteLine($"\nEnter new score for Track {inputTrackId} (3 - {nrBirds}): ");

                            string newScoreStr = Console.ReadLine();

                            try
                            {
                                newScore = int.Parse(newScoreStr);

                                if (newScore >= 3 && newScore <= nrBirds)
                                {
                                    int gameId = GetGameId(playerId, inputTrackId);
                                    UpdateGameScore(gameId, playerId, inputTrackId, newScore);
                                }
                                loop = true;
                                Console.Clear();
                                ListTracks(playerName);
                            }
                            catch
                            {
                                loop = false;
                            }
                        }
                    }

                    else
                    {
                        Console.Clear();
                        ListTracks(playerName);
                    }
                }
                catch
                {
                    Console.Clear();
                    ListTracks(playerName);
                }
            }

            Console.ReadLine();
        }

        private static int GetPlayerId(string playerName)
        {
            using (var context = new GameContext())
            {
                var queryPlayerId = (from x in context.Players
                                     where x.PlayerName == playerName
                                     select x.PlayerId).First();
                return queryPlayerId;
            }
        }

        private static int GetNextPlayerId()
        {
            using (var context = new GameContext())
            {
                var queryMaxPlayerId = (from x in context.Players
                                        orderby x.PlayerId descending
                                        select x.PlayerId).First();

                int nextPlayerId = queryMaxPlayerId + 1;
                return nextPlayerId;
            }
        }

        private static string GetPlayerName(int playerId)
        {
            using (var context = new GameContext())
            {
                var queryPlayerName = (from x in context.Players
                                       where x.PlayerId == playerId
                                       select x.PlayerName).First();
                return queryPlayerName;
            }
        }

        private static int GetCurrentTrackId(int playerId)
        {
            using (var context = new GameContext())
            {
                var queryTrackIdMax = (from x in context.Games
                                       where x.PlayerId == playerId
                                       orderby x.TrackId descending
                                       select x.TrackId).First();

                return queryTrackIdMax;
            }
        }

        private static int[] GetScore(int playerId, int trackId)
        {
            using (var context = new GameContext())
            {
                var queryGameScore = (from x in context.Games
                                      where x.PlayerId == playerId && x.TrackId == trackId
                                      select x.GameScore).First();

                int nrBirds = GetNrBirds(trackId);

                int nrBirdsLeft = nrBirds - queryGameScore;

                int[] getScoreNrLeft = new int[] { queryGameScore, nrBirdsLeft };

                return getScoreNrLeft;
            }
        }

        private static List<object> GetHighScore(int trackId)
        {
            using (var context = new GameContext())
            {
                var queryTrackHighScore = (from x in context.Games
                                           where x.TrackId == trackId
                                           orderby x.GameScore
                                           select x.GameScore).First();

                var queryPlayerIdHighScore = (from x in context.Games
                                              where x.TrackId == trackId && x.GameScore == queryTrackHighScore
                                              select x.PlayerId).First();

                string playerNameHighScore = GetPlayerName(queryPlayerIdHighScore);

                List<object> getNameAndHighScore = new List<object> { playerNameHighScore, queryTrackHighScore };

                return getNameAndHighScore;
            }
        }

        private static int GetNrBirds(int trackId)
        {
            using (var context = new GameContext())
            {
                var queryNrBirds = (from x in context.Tracks
                                    where x.TrackId == trackId
                                    select x.NrBird).First();

                return queryNrBirds;
            }
        }

        

        private static void AddTrack(string playerName)
        {
            using (var context = new GameContext())
            {
                var valueTrackIdTracks = (from x in context.Tracks
                                          orderby x.TrackId descending
                                          select x.TrackId).First();

                int playerId = GetPlayerId(playerName);

                var queryTrackIdExist = (from x in context.Games
                                       where x.PlayerId == playerId
                                       select x.TrackId).Count();

                int valueTrackIdGame = 0;

                if (queryTrackIdExist > 0)
                {
                    var queryTrackIdGame = (from x in context.Games
                                            where x.PlayerId == playerId
                                            orderby x.TrackId descending
                                            select x.TrackId).First();

                    valueTrackIdGame = queryTrackIdGame;
                }

                if (valueTrackIdGame < valueTrackIdTracks)
                {
                    int nextTrackId = valueTrackIdGame + 1;

                    var queryNrBridsNextTrack = (from x in context.Tracks
                                                 where x.TrackId == nextTrackId
                                                 select x.NrBird).First();

                    CreateGame(playerId, nextTrackId, queryNrBridsNextTrack);

                    Console.Clear();
                    ListTracks(playerName);
                }

                else
                {
                    int nextTrackId = valueTrackIdTracks + 1;
                    CreateTrack(playerId, nextTrackId);
                }
            }

        }


        private static void CreateTrack(int playerId, int nextTrackId)
        {
            int nrBirds = 20;
            bool loop = false;

            while (loop != true)
            {
                Console.WriteLine($"Enter value for number birds for Tack {nextTrackId} (3-20):");
                string nrBirdsStr = Console.ReadLine();

                try
                {
                    nrBirds = int.Parse(nrBirdsStr);

                    if (nrBirds >= 3 && nrBirds <= 20)
                    {
                        using (var context = new GameContext())
                        {
                            context.Tracks.Add(new Track()
                            {
                                TrackId = nextTrackId,
                                NrBird = nrBirds
                            });

                            context.SaveChanges();
                        }

                    }

                    CreateGame(playerId, nextTrackId, nrBirds);

                    string playerName = GetPlayerName(playerId);

                    loop = true;
                                       
                }
                catch
                {
                    Console.WriteLine($"You entered: {nrBirdsStr}, enter value for number birds for Tack {nextTrackId} (3-20):");
                    loop = false;
                }

                finally
                {
                    Console.Clear();
                    string playerName = GetPlayerName(playerId);

                    ListTracks(playerName);
                }
            }

        }

        private static void CreateGame(int playerId, int nextTrackId, int nrBirds)
        {
            int nextGameId = GetNextGameId();

            using (var context = new GameContext())
            {
                context.Games.Add(new Game()
                {
                    GameId = nextGameId,
                    PlayerId = playerId,
                    TrackId = nextTrackId,
                    GameScore = nrBirds
                });

                context.SaveChanges();
            }
        }

    private static void UpdateGameScore(int gameId, int playerId, int trackId, int gameScore)
        {
            using (var context = new GameContext())
            {
                context.Games.AddOrUpdate(new Game()
                {
                    GameId = gameId,
                    PlayerId = playerId,
                    TrackId = trackId,
                    GameScore = gameScore
                });

                context.SaveChanges();
            }
        }

        private static int GetNextGameId()
        {
            using (var context = new GameContext())
            {
                var valueGameId = (from x in context.Games
                                   orderby x.GameId descending
                                   select x.GameId).First();

                int nextGameId = valueGameId + 1;

                return nextGameId;
            }

        }

        private static int GetGameId(int playerId, int trackId)
        {
            using (var context = new GameContext())
            {
                var queryGameId = (from x in context.Games
                                   where x.PlayerId == playerId && x.TrackId == trackId
                                   select x.GameId).First();

                return queryGameId;
            }

        }
    }
}
