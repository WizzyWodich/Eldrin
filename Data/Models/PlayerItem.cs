using System.ComponentModel.DataAnnotations;
using Eldrin.Data.Models;

public class PlayerItem
{
    [Key]
    public uint Id { get; set; }

    public uint PlayerId { get; set; }  // Внешний ключ для игрока
    public uint ItemId { get; set; }    // Внешний ключ для предмета

    public bool IsEquipped { get; set; } // Для отслеживания экипировки

    // Связь с игроком и предметом
    public Player Player { get; set; }
    public Item Item { get; set; }
}
