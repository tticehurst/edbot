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

    public static async Task Main() {
        DotNetEnv.Env.Load();

        _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 100}); 
        var interactionService = new InteractionService(_client.Rest);
        var interactionHandler = new InteractionHandler(_client, interactionService, new EmptyServiceProvider());
        var eventHandler = new BotEventHandler(_client, interactionService);

        await interactionHandler.InitializeAsync();
        eventHandler.Initialize();

        _client.Log += Log; 

        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg) {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private sealed class EmptyServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType) => null;
    }
}