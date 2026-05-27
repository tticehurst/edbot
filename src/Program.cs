using System;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using edbot.Commands;
using edbot.Events;
using Microsoft.Extensions.DependencyInjection;

namespace edbot;

public class Program {
    private static DiscordSocketClient? _client; 
    private static InteractionHandler? _interactionHandler;
    private static BotEventHandler? _eventHandler;

    public static async Task Main() {
        DotNetEnv.Env.Load();

        _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 100}); 
        var interactionService = new InteractionService(_client.Rest);
        var services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(interactionService)
            .BuildServiceProvider();

        _interactionHandler = new InteractionHandler(_client, interactionService, services);
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
}