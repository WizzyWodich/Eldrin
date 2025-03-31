using Discord;
using Discord.Interactions;
using System.IO;
using System.Threading.Tasks;

public class MapCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("map", "Показывает карту")]
    public async Task MapAsync()
    {
        var mapPath = "./assets/mainMap.png"; 
        if (!File.Exists(mapPath))
        {
            await RespondAsync("Карта не найдена.");
            return;
        }

        var embed = new EmbedBuilder()
            .WithTitle("Карта")
            .WithColor(Color.Blue)
            .Build();

        await RespondAsync(embed: embed);
        await FollowupWithFileAsync(mapPath, "mainMap.png");
    }
}
