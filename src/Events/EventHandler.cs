// src/Events/EventHandler.cs
using System;
using System.Linq;
using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;

namespace edbot.Events;

public interface IBotEvent
{
    void Register(DiscordSocketClient client, InteractionService interactionService);
}

public class BotEventHandler(DiscordSocketClient client,InteractionService interactionService)
{
    private readonly DiscordSocketClient _client = client;
    private readonly InteractionService _interactionService = interactionService;

    public void Initialize() {
        var eventTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                typeof(IBotEvent).IsAssignableFrom(type) &&
                type.Namespace == "edbot.Events");

        foreach (var eventType in eventTypes)
        {
            if (Activator.CreateInstance(eventType) is not IBotEvent botEvent)
            {
                throw new InvalidOperationException(
                    $"Failed to instantiate event type {eventType.Name}. Ensure it has a parameterless constructor.");
            }

            botEvent.Register(_client, _interactionService);
        }
    }
}