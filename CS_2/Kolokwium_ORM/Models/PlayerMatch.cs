using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_ORM.Models;

[PrimaryKey(nameof(MatchId),nameof(PlayerId))]
public class PlayerMatch

{
    [ForeignKey(nameof(match))]
    public int MatchId { get; set; }
    [ForeignKey(nameof(player))]
    public int PlayerId { get; set; }
    public int MVPs { get; set; }
    
    [Column(TypeName = "decimal")]
    [Precision(5, 2)]
    public decimal Rating { get; set; }
    
    
    public Match match { get; set; }
    public Player player { get; set; }
    
    
    
}