using Kolokwium_ORM.Models;

namespace Kolokwium_ORM.DTOs;

public class PostDto
{
  public class InsertPlayerDTO
  {
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime birthDate { get; set; }
    public ICollection<InsertMatchesDTO> InsertMatechesDTO;
  }
  
  public class InsertMatchesDTO
  {
    public int matchId { get; set; }
    public int MVPs { get; set; }
    public int rating { get; set; }

  }
}

