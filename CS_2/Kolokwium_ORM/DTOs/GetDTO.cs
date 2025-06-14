using Kolokwium_ORM.Models;

namespace Kolokwium_ORM.DTOs;

public class GetDTO
{
  public class PlayerDTO
  {
    public int playerId { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime birthDate { get; set; }
    public ICollection<MatchDTO> matches { get; set; }
  }
  
  public class MatchDTO
  {
    public string tournament { get; set; }
    public string map { get; set; }
    public DateTime date { get; set; }
    public int MVPs { get; set; }
    public decimal rating { get; set; }
    public int team1Score { get; set; }
    public int team2Score { get; set; }
  }
}

