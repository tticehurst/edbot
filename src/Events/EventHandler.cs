// src/Events/EventHandler.cs
using System.Threading.Tasks;
using Discord.Interactions;
using Discord.WebSocket;

namespace edbot.Events;

public class BotEventHandler(DiscordSocketClient client,InteractionService interactionService)
{
    private readonly DiscordSocketClient _client = client;
    private readonly InteractionService _interactionService = interactionService;
    public void Initialize() {
        // Hook up our events to the client here
        _client.Ready += () => ReadyEvent.Handler(_client,_interactionService);
        // _client.MessageReceived += MessageReceivedEvent.Handler;
    }
}