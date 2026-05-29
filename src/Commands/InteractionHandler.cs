using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace edbot.Commands;

public class InteractionHandler(DiscordSocketClient client, InteractionService interactionService, IServiceProvider services)
{
    private readonly DiscordSocketClient _client = client;
    private readonly InteractionService _handler = interactionService;   
    private readonly IServiceProvider _services = services;

    public async Task InitializeAsync() {
        await _handler.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        _client.InteractionCreated += HandleInteraction;
        _handler.InteractionExecuted += HandleInteractionExecuted;
    }

    private async Task HandleInteraction(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client,interaction);
            await _handler.ExecuteCommandAsync(context, _services);
        }catch (Exception ex)
        {
            Console.WriteLine($"Error handling interaction: {ex}");
            if (interaction.Type == InteractionType.ApplicationCommand)
                await interaction.GetOriginalResponseAsync().ContinueWith(async (msg) => await interaction.RespondAsync($"An error occurred while executing this command.\n{ex.ToString()}", ephemeral: true));
        }
    }

    private Task HandleInteractionExecuted(ICommandInfo info, IInteractionContext context, IResult result)
    {
        if (!result.IsSuccess)
        {
            Console.WriteLine($"Command Error [{result.Error}]: {result.ErrorReason}");
        }
        return Task.CompletedTask;
    }
}