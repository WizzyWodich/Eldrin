using Discord.Interactions;

namespace Eldrin.Bot.Modules;

public class TimeCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("time", "Показывает текущее время")]
    public async Task TimeAsync()
    {
        var time = DateTime.Now.ToString("HH:mm:ss");
        await RespondAsync($"Текущее время: {time}");
    }
}