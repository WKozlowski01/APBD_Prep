using Kolokwium_ORM.Data;
using Kolokwium_ORM.DTOs;
using Kolokwium_ORM.Exceptions;
using Kolokwium_ORM.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_ORM.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<GetDTO.RacerDTO> GetRacer(int RacerId)
    {
        var racer = await _context.Racers
            .Where(r => r.RacerId == RacerId)
            .Include(r => r.RaceParticipations)
            .ThenInclude(rp => rp.trackrace)
            .ThenInclude(tr => tr.race)
            .Include(r => r.RaceParticipations)
            .ThenInclude(rp => rp.trackrace)
            .ThenInclude(tr => tr.track)
            .FirstOrDefaultAsync();
        
           
        var result = new GetDTO.RacerDTO
        {
            RacerId = racer.RacerId,
            FirstName = racer.FirstName,
            LastName = racer.LastName,
            RaceParticipations = racer.RaceParticipations
                .Select(r=> new GetDTO.RaceParticipationDTO
                    {
                  race  = new GetDTO.RaceDTO
                  {
                      name = r.trackrace.race.Name,
                      Location = r.trackrace.race.Location,
                      date = r.trackrace.race.datetime
                },
                  track = new GetDTO.TrackDTO
                      {
                          name = r.trackrace.track.Name,
                          lengthInKm = r.trackrace.track.LengthlnKm
                      },
                  Laps = r.trackrace.Laps,
                  FinishTimeLnSeconds = r.FinishTimelnSeconds,
                  Position = r.Position
                  }).ToList()
            
        };

        return result;


    }

    public async Task InsertRacer(PostDto data)
    {

        var ifRaceExists = await _context.Races
            .Where(r => r.Name.Equals(data.raceName))
            .FirstOrDefaultAsync();

        if (ifRaceExists == null)
        {
            throw new NoraceException();
        }


        var ifTrackExists = await _context.Tracks
            .Where(t => t.Name.Equals(data.trackName)).FirstOrDefaultAsync();
        if (ifTrackExists == null)
        {
            throw new NoTrackExcepion();
        }

        var racerIds = data.participations.Select(p => p.racerId).ToList();
        var existingRacers = await _context.Racers
            .Where(r => racerIds.Contains(r.RacerId))
            .Select(r => r.RacerId)
            .ToListAsync();

        var missingRacers = racerIds.Except(existingRacers).ToList();
        if (missingRacers.Any())
        {
            throw new NoRacerException(); 
        }

        var praticipationList = new List<RaceParticipation>();


        var trackRaceEntity = await _context.TrackRaces
            .Include(r=>r.race)
            .Include(t=>t.track)
            .Where(tn=>tn.track.Name.Equals(data.trackName) && tn.race.Name.Equals(data.raceName))
            .FirstOrDefaultAsync();
        
        foreach (var innerData in data.participations)
        {
            
            var raceParticipation = new RaceParticipation
            {
                TrackRaceId = _context.TrackRaces
                    .Where(tr => tr.track.Name.Equals(data.trackName))
                    .Where(tr => tr.race.Name.Equals(data.raceName))
                    .Select(i => i.TrackRaceId).FirstOrDefault(),
                RacerId = innerData.racerId,
                FinishTimelnSeconds = innerData.finishTimeInSeconds,
                Position = innerData.position,
            };
            if (innerData.finishTimeInSeconds < trackRaceEntity.BestTimelnSeconds)
            {
                trackRaceEntity.BestTimelnSeconds = innerData.finishTimeInSeconds;
            }

            praticipationList.Add(raceParticipation);
        }

        _context.RaceParticipations.AddRange(praticipationList);
        await _context.SaveChangesAsync();
        
    }
}