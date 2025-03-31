using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Eldrin.Data.Models
{
    public class Corpse
{
    [Key]
    public uint Id { get; set; }
    
    // Связь с игроком
    public uint PlayerId { get; set; }
    public Player Player { get; set; }  // Добавьте это свойство для связи с Player
    
    public DateTime DeathTime { get; set; }
    public string Location { get; set; }
    public ICollection<Item> Items { get; set; }
}

}
