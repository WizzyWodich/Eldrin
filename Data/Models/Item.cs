using System.ComponentModel.DataAnnotations;

namespace Eldrin.Data.Models
{
    public class Item
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }  // Например, меч, броня и т.д.
        public int Durability { get; set; } // Крепкость
        public int Damage { get; set; } // Урон
        public int Value { get; set; } // Цена

        // Для связи с инвентарем через промежуточную таблицу PlayerItem
        public ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
    }
}
