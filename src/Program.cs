using System;
using System.Threading.Tasks; // Added for Task
using Discord;
using Discord.WebSocket;

namespace edbot.src;

public class Program {
    private static DiscordSocketClient? _client; 

    public static async Task Main() {
        DotNetEnv.Env.Load();

        _client = new DiscordSocketClient(); 
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