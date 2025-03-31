using System.ComponentModel.DataAnnotations;
using Eldrin.Data.Models;

namespace Eldrin.Data.Models;

public class Race
{
    [Key]
    public uint Id { get; set; }
    public string Name { get; set; }
    public string StartingCity { get; set; }

    public int HealthBonus { get; set; } = 0;  
    public int StrengthBonus { get; set; } = 0; 
    public int AgilityBonus { get; set; } = 0;  
    public int MagicBonus { get; set; } = 0;   
}
