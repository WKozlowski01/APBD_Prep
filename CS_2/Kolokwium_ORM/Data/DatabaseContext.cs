using Kolokwium_ORM.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_ORM.Data;

public class DatabaseContext:DbContext
{
    public DbSet<Map> Maps { get; set; }
     public DbSet<Match> Matches { get; set; }
    public DbSet<Player> Players { get; set; }
     public DbSet<PlayerMatch> PlayerMatches { get; set; }
     public DbSet<Tournament> Tournaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Map>().HasData(new List<Map>()
    {
        new Map() { MapId = 1, Name = "Map 1" , Type = "ffa" },
      
    });
    
    modelBuilder.Entity<Match>().HasData(new List<Match>()
    {
        new Match()
        {
            MatchId = 1,TournamentId = 1,MapId = 1,MatchDate = DateTime.Today,Team1Score = 5,Team2Score = 3
        },
       
    });
    
    modelBuilder.Entity<Player>().HasData(new List<Player>()
    {
        new Player()
        {
            PlayerId = 1, FirstName = "Jarosław", LastName = "Jarząbkowski", BirthDate = new DateTime(1990,07,21)
        },
       
    });
    
    modelBuilder.Entity<PlayerMatch>().HasData(new List<PlayerMatch>()
    {
        new PlayerMatch()
        {
           MatchId = 1, PlayerId = 1, MVPs = 1, Rating = (decimal)5.6
        },
       
    });
    
    modelBuilder.Entity<Tournament>().HasData(new List<Tournament>()
    {
       new Tournament()
       {
           TournamentId = 1, Name = "Wielki turniej", StartDate = DateTime.Today, EndDate = DateTime.Today
       }
      
    });
}
    
}