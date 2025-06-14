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


    public async Task<GetDTO.PlayerDTO> GetPlayerMatchesAsync(int playerId)
    {
        var player = await _context.Players
            .Include(pm=>pm.PlayerMatches)
            .ThenInclude(m=>m.match)
            .ThenInclude(t=>t.Tournament)
            .Include(pm=>pm.PlayerMatches)
            .ThenInclude(m=>m.match)
            .ThenInclude(t=>t.Map)
            .FirstOrDefaultAsync(p => p.PlayerId == playerId);


        var result = new GetDTO.PlayerDTO
        {
            playerId = playerId,
            firstName = player.FirstName,
            lastName = player.LastName,
            birthDate = player.BirthDate,
            matches = player.PlayerMatches.Select(pm => new GetDTO.MatchDTO
            {
                tournament = pm.match.Tournament.Name,
                map = pm.match.Map.Name,
                date = pm.match.MatchDate,
                MVPs = pm.MVPs,
                rating = pm.Rating,
                team1Score = pm.match.Team1Score,
                team2Score = pm.match.Team2Score,
            }).ToList()
        };
      return result;
    }

    public async Task AddPlayerMatchAsync(PostDto.InsertPlayerDTO data)
    {


    var player =await _context.Players
        .Include(pm=>pm.PlayerMatches)
        .Where(f=>f.FirstName.Equals(data.firstName) && f.LastName.Equals(data.lastName))
        .FirstOrDefaultAsync();
    
        

        var matchesFromDB = await _context.PlayerMatches
            .Include(pm => pm.match).ToListAsync();

        var matchesFromDBIds = matchesFromDB.Select(i => i.MatchId).ToList();
            
            
        var matchesIdsFromQuery = data.InsertMatechesDTO
            .Select(i => i.matchId).ToList();
        
        var matchesDiff = matchesFromDBIds.Except(matchesIdsFromQuery).ToList();
       
        if (matchesDiff.Any())
        {
            throw new NoMatchException();
        }

        var newPlayer = new Player
        {
            PlayerId = player.PlayerId,
            FirstName = data.firstName,
            LastName = data.lastName,
            BirthDate = data.birthDate,
            PlayerMatches = new List<PlayerMatch>()
        };

        await _context.AddAsync(newPlayer);
        
        
        
        foreach (var match in data.InsertMatechesDTO)
        {

            var playerMatch = await _context.PlayerMatches
                .Where(i => i.PlayerId == player.PlayerId && i.MatchId==match.matchId)
                .FirstOrDefaultAsync();


            if (playerMatch.Rating < match.rating)
            {
                playerMatch.Rating = match.rating;
            }


            player.PlayerMatches.Add(new PlayerMatch
            {
                MatchId = match.matchId,
                PlayerId =player.PlayerId,
                MVPs = match.MVPs,
                Rating = match.rating
            });    
            
        }
        
        await _context.SaveChangesAsync();
        
        




    }
}