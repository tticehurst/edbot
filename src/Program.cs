using System;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using edbot.Commands;
using edbot.Events;

namespace edbot;

public class Program {
    private static DiscordSocketClient? _client; 
    private static InteractionHandler? _interactionHandler;
    private static BotEventHandler? _eventHandler;

    public static async Task Main() {
        DotNetEnv.Env.Load();

        _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 100}); 
        var interactionService = new InteractionService(_client.Rest);
        _interactionHandler = new InteractionHandler(_client, interactionService, new NullServiceProvider());
        _eventHandler = new BotEventHandler(_client, interactionService);

        await _interactionHandler.InitializeAsync();
        _eventHandler.Initialize();

        _client.Log += Log; 

        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg) {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private sealed class NullServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType) => null;
    }
}