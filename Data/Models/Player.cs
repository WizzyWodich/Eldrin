using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Eldrin.Data.Models
{
    public class Player
{
    [Key]
    public uint Id { get; set; }

    [Required]
    public string DiscordId { get; set; }

    public int Hunger { get; set; } = 100;
    public int Level { get; set; } = 1;
    public int Gold { get; set; } = 100;
    public int Health { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Agility { get; set; } = 10;
    public int Magic { get; set; } = 10;

    public uint RaceId { get; set; }
    public Race Race { get; set; }

    public string StartingCity => Race?.StartingCity;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Связь с инвентарем
    public ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
    public ICollection<Corpse> Corpses { get; set; } = new List<Corpse>();
}

}
