using Eldrin.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eldrin.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Corpse> Corpses { get; set; }
        public DbSet<PlayerItem> PlayerItems { get; set; }
        public DbSet<Race> Races { get; set; }

        public GameContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Инициализация начальных данных для рас
    modelBuilder.Entity<Race>().HasData(
        new Race { Id = 1, Name = "Эльф", StartingCity = "Эльфийский Лес", HealthBonus = 10, AgilityBonus = 5, MagicBonus = 5 },
        new Race { Id = 2, Name = "Человек", StartingCity = "Город Людей", HealthBonus = 0, StrengthBonus = 5 },
        new Race { Id = 3, Name = "Гном", StartingCity = "Гномий Форпост", HealthBonus = 20, StrengthBonus = 5 },
        new Race { Id = 4, Name = "Лизардмен", StartingCity = "Болотные Хижины", HealthBonus = 15, StrengthBonus = 5, AgilityBonus = 3 }
    );

    // Связь Player с PlayerItem (все предметы игрока)
    modelBuilder.Entity<Player>()
        .HasMany(p => p.PlayerItems)  // Связь с предметами игрока
        .WithOne(pi => pi.Player)    // Один игрок может иметь много предметов
        .HasForeignKey(pi => pi.PlayerId) // Внешний ключ
        .OnDelete(DeleteBehavior.Cascade);  // Удаление игрока удаляет предметы

    // Связь PlayerItem с Item (каждый предмет может быть связан с несколькими PlayerItem)
    modelBuilder.Entity<PlayerItem>()
        .HasOne(pi => pi.Item)  // Каждый PlayerItem связан с одним Item
        .WithMany(i => i.PlayerItems)  // Один предмет может быть связан с несколькими PlayerItems
        .HasForeignKey(pi => pi.ItemId);  // Внешний ключ для предмета
}



    }
}