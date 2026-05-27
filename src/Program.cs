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
    private InteractionService? _interactionService;

    public static async Task Main() {
        DotNetEnv.Env.Load();

        _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 100}); 
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