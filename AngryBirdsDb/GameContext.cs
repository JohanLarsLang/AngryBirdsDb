using AngryBirdsDb.Models;
using System.Data.Entity;
using System.Linq;
using System;

namespace AngryBirdsDb
{
    public class GameContext : DbContext
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AngryBirdsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public GameContext() : base(connectionString) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Game> Games { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Player>().Property(p => p.PlayerName).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Game>().HasKey(l => l.GameId);

         modelBuilder.Entity<Game>().HasRequired(m => m.Player).WithMany(p => p.Games).WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>().HasRequired(m => m.Track).WithMany(p => p.Games).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Game>().ToTable("Game");

            base.OnModelCreating(modelBuilder);
            
            
        }
    }
}
