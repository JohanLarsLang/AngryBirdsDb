using System.Data.Entity;

namespace AngryBirdsDb
{
    public class GameContext : DbContext
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AngryBirdsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public GameContext() : base(connectionString) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<GameList> GameLists { get; set; }
    }
}
