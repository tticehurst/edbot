// src/Events/Ready.cs
using System;
using System.Threading.Tasks;
using Discord.Interactions;
using Discord.WebSocket;

namespace edbot.Events;

public static class ReadyEvent {
    public static async Task Handler(DiscordSocketClient client, InteractionService interactionService) 
    {
        await interactionService.RegisterCommandsToGuildAsync(185366755277799424UL);
        
        Console.WriteLine($"Commands registered. Logged in as: {client.CurrentUser.Username}");
    }
}