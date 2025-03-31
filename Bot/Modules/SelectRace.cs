using Discord;
using Discord.Interactions;
using Eldrin.Data.Repositories;
using Eldrin.Data.Models;
using Eldrin.Core.Contracts;

namespace Eldrin.Bot.Modules;

public class SelectRace : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IPlayerRepository _playerRepository;

    public SelectRace(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    [SlashCommand("selectrace", "Выбор расы для нового персонажа")]
    public async Task SelectRaceCommand()
    {
        try
        {
            var menu = new SelectMenuBuilder()
                .WithPlaceholder("Выберите расу...")
                .WithCustomId("select_race_menu")
                .AddOption("Эльф", "1", "Ловкие и магические создания")
                .AddOption("Человек", "2", "Сбалансированные бойцы")
                .AddOption("Гном", "3", "Сильные и выносливые воины")
                .AddOption("Лизардмен", "4", "Гибкие и хитрые воины болот");

            var builder = new ComponentBuilder().WithSelectMenu(menu);

            await RespondAsync("Выберите вашу расу:", components: builder.Build());
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    [ComponentInteraction("select_race_menu")]
    public async Task OnRaceSelected(string selectedRaceId)
    {
        try
        {
            if (!uint.TryParse(selectedRaceId, out uint raceId))
            {
                await RespondAsync("Ошибка: неверный формат ID расы.", ephemeral: true);
                return;
            }

            var race = await _playerRepository.GetRaceById(raceId);
            if (race == null)
            {
                await RespondAsync("Ошибка: выбранная раса не найдена.", ephemeral: true);
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle($"Вы выбрали расу {race.Name}")
                .WithDescription($"**Город старта:** {race.StartingCity}\n\n" +
                                 $"**Бонусы:**\n" +
                                 $"- Здоровье: {race.HealthBonus}\n" +
                                 $"- Сила: {race.StrengthBonus}\n" +
                                 $"- Ловкость: {race.AgilityBonus}\n" +
                                 $"- Магия: {race.MagicBonus}")
                .WithColor(Color.Blue)
                .Build();

            var confirmButton = new ButtonBuilder()
                .WithLabel("Подтвердить")
                .WithStyle(ButtonStyle.Success)
                .WithCustomId($"confirm_race_{raceId}");

            var backButton = new ButtonBuilder()
                .WithLabel("Назад")
                .WithStyle(ButtonStyle.Danger)
                .WithCustomId("back_to_race_selection");

            var actionButtons = new ComponentBuilder()
                .WithButton(confirmButton)
                .WithButton(backButton)
                .Build();

            await RespondAsync(embed: embed, components: actionButtons);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    [ComponentInteraction("confirm_race_*")]
    public async Task OnConfirmRace(string raceIdStr)
    {
        try
        {
            if (!uint.TryParse(raceIdStr, out uint raceId))
            {
                await RespondAsync("Ошибка: неверный формат расы.", ephemeral: true);
                return;
            }

            var player = await _playerRepository.CreatePlayer(Context.User.Id.ToString(), raceId);
            var embed = new EmbedBuilder()
                .WithTitle("Поздравляем!")
                .WithDescription($"Вы выбрали расу **{player.Race.Name}** и теперь находитесь в **{player.Race.StartingCity}**.")
                .WithColor(Color.Green)
                .Build();

            await RespondAsync(embed: embed);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    [ComponentInteraction("back_to_race_selection")]
    public async Task OnBackToRaceSelection()
    {
        try
        {
            await SelectRaceCommand();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    /// <summary>
    /// Глобальная обработка ошибок.
    /// </summary>
    private async Task HandleErrorAsync(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Ошибка] {ex.Message}");
        Console.ResetColor();

        await RespondAsync("Вы уже выбрали расу.", ephemeral: true);
    }
}
