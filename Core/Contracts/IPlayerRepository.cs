using Eldrin.Data.Models;

namespace Eldrin.Core.Contracts;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByDiscordId(string discordId);
    Task<Race?> GetRaceById(uint id);
    Task<Player> CreatePlayer(string discordId, uint raceId);
    Task<bool> DeletePlayerByDiscordId(string discordId);
    Task<List<Player>> GetAllPlayers();
}
