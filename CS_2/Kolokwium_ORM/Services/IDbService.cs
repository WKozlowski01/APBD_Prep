using Kolokwium_ORM.DTOs;

namespace Kolokwium_ORM.Services;

public interface IDbService
{
   public Task<GetDTO.PlayerDTO> GetPlayerMatchesAsync(int playerId);
   public Task AddPlayerMatchAsync(PostDto.InsertPlayerDTO data);

}