using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, IServiceProvider services)
    {
        _client = client;
        _services = services;

        _commands = new InteractionService(client);


        _client.Ready += ReadyAsync;
        _client.InteractionCreated += HandleCommandAsync;
    }


    private async Task ReadyAsync()
    {
        await Task.Delay(5000);
        await InstallCommandsAsync();
    }

    public async Task InstallCommandsAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);


        ulong GuildID = 1155172196071780412;
        await _commands.RegisterCommandsToGuildAsync(GuildID);
        //foreach (var guild in _client.Guilds)
        //{
        //    await _commands.RegisterCommandsToGuildAsync(guild.Id);
        //}
    }

    private async Task HandleCommandAsync(SocketInteraction interaction)
    {
        var context = new SocketInteractionContext(_client, interaction);
        await _commands.ExecuteCommandAsync(context, _services);
    }
}