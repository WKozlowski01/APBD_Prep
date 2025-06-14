using Kolokwium_ORM.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_ORM.Data;

public class DatabaseContext:DbContext
{
    public DbSet<Race> Races { get; set; }
     public DbSet<RaceParticipation> RaceParticipations { get; set; }
    public DbSet<Racer> Racers { get; set; }
     public DbSet<Track> Tracks { get; set; }
     public DbSet<TrackRace> TrackRaces { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Race>().HasData(new List<Race>()
    {
        new Race() { raceid = 1, Name = "abc", Location = "Monza", datetime = DateTime.Today },
      
    });
    
    modelBuilder.Entity<RaceParticipation>().HasData(new List<RaceParticipation>()
    {
        new RaceParticipation()
        {
            TrackRaceId = 1, RacerId = 1 ,FinishTimelnSeconds = 20,
            Position = 1
        },
       
    });
    
    modelBuilder.Entity<Racer>().HasData(new List<Racer>()
    {
        new Racer()
        {
            RacerId = 1, FirstName = "Alan", LastName = "Prost", 
        },
       
    });
    
    modelBuilder.Entity<Track>().HasData(new List<Track>()
    {
        new Track()
        {
            TrackId = 1, Name = "Monza", LengthlnKm = 3
        },
       
    });
    
    modelBuilder.Entity<TrackRace>().HasData(new List<TrackRace>()
    {
        new TrackRace() { TrackRaceId = 1, TrackId = 1,RaceId = 1,Laps = 10,BestTimelnSeconds = 56},
      
    });
}
    
}