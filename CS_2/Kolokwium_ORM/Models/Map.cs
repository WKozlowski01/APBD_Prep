using System.ComponentModel.DataAnnotations;

namespace Kolokwium_ORM.Models;

public class Map
{
    [Key]
    public int MapId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    
    public ICollection<Match> Matches { get; set; }
    
    
}