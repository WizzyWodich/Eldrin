using Eldrin.Data;
using Eldrin.Core.Contracts;
using Eldrin.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eldrin.Data.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly GameContext _context;

    public PlayerRepository(GameContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает игрока по Discord ID.
    /// </summary>
    public async Task<Player?> GetPlayerByDiscordId(string discordId)
    {
        return await _context.Players
            .Include(p => p.Race) 
            .FirstOrDefaultAsync(p => p.DiscordId == discordId);
    }

    /// <summary>
    /// Получает расу по ID.
    /// </summary>
    public async Task<Race?> GetRaceById(uint id)
    {
        return await _context.Races.FindAsync(id);
    }

    /// <summary>
    /// Создаёт нового игрока в БД.
    /// </summary>
    public async Task<Player> CreatePlayer(string discordId, uint raceId)
    {
        var existingPlayer = await GetPlayerByDiscordId(discordId);
        if (existingPlayer != null)
        {
            throw new Exception("У вас уже есть персонаж!");
        }

        var race = await GetRaceById(raceId);
        if (race == null) throw new Exception("Выбранная раса не найдена.");

        var player = new Player
        {
            DiscordId = discordId,
            RaceId = race.Id,
            Hunger = 100,
            Level = 1,
            Gold = 100,
            Health = 100 + race.HealthBonus,
            Strength = 10 + race.StrengthBonus,
            Agility = 10 + race.AgilityBonus,
            Magic = 10 + race.MagicBonus,
            CreatedAt = DateTime.UtcNow
        };

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return player;
    }

    /// <summary>
    /// Удаляет игрока по Discord ID.
    /// </summary>
    public async Task<bool> DeletePlayerByDiscordId(string discordId)
    {
        var player = await GetPlayerByDiscordId(discordId);
        if (player == null) return false;

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Получает список всех игроков.
    /// </summary>
    public async Task<List<Player>> GetAllPlayers()
    {
        return await _context.Players.Include(p => p.Race).ToListAsync();
    }
}
