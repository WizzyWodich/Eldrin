using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Eldrin.Bot
{
    public class Bot
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;
        private readonly string _token;
        private readonly ILogger<Bot> _logger;

        public Bot(
                    IConfiguration configuration, 
                    DiscordSocketClient client, 
                    CommandHandler commandHandler, 
                    ILogger<Bot> logger,
                    IServiceProvider services) 
        {
            _client = client;
            _token = configuration["Discord:Token"];
            _logger = logger;
            _services = services; // Теперь _services корректный

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;

            _commands = new InteractionService(_client);
        }

        public async Task RunAsync()
        {
            try
            {
                var commandHandler = _services.GetRequiredService<CommandHandler>(); // Теперь _services корректный

                await _client.LoginAsync(TokenType.Bot, _token);
                await _client.SetStatusAsync(UserStatus.Online);
                await _client.SetGameAsync("Eldrin", null, ActivityType.Playing);

                await _client.StartAsync();
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запуске бота.");
                Environment.Exit(1);  
            }
        }

        private Task ReadyAsync()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[ {DateTime.Now} ] ");
            Console.ResetColor();
            Console.WriteLine($"[ Запуск клиента ] {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }

        private Task LogAsync(LogMessage message)
        {
            Console.WriteLine(message);
            if (message.Exception is not null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[ {DateTime.Now} ] ");
                Console.ResetColor();
                Console.WriteLine(message.Exception);
            }
            return Task.CompletedTask;
        }
    }
}
