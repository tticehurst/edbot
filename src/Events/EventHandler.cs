// src/Events/EventHandler.cs
using System.Threading.Tasks;
using Discord.WebSocket;

namespace edbot.Events;

public class BotEventHandler {
    private readonly DiscordSocketClient _client;

    public BotEventHandler(DiscordSocketClient client) {
        _client = client;
    }

    public void Initialize() {
        // Hook up our events to the client here
        _client.Ready += ReadyEvent.Handler;
        // _client.MessageReceived += MessageReceivedEvent.Handler;
    }
}