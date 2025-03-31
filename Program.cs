using Discord.WebSocket;
using Eldrin.Bot;
using Eldrin.Data;
using Eldrin.Core.Contracts;
using Eldrin.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<GameContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton<DiscordSocketClient>();
                services.AddSingleton<CommandHandler>();
                services.AddScoped<IPlayerRepository, PlayerRepository>();
                services.AddSingleton<Bot>();

                services.AddLogging();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var bot = services.GetRequiredService<Bot>();
        await bot.RunAsync();
    }
}
