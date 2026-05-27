using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using edbot.Events;

namespace edbot;

public class Program {
    private static DiscordSocketClient? _client; 

    public static async Task Main() {
        DotNetEnv.Env.Load();

        _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 100}); 
        _client.Log += Log; 

        new BotEventHandler(_client).Initialize();

        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg) {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}