// src/Events/Ready.cs
using System;
using System.Threading.Tasks;
using Discord.Interactions;
using Discord.WebSocket;

namespace edbot.Events;

public class ReadyEvent : IBotEvent
{
    public void Register(DiscordSocketClient client, InteractionService interactionService)
    {
        client.Ready += async () => await Handler(client, interactionService);
    }

    public static async Task Handler(DiscordSocketClient client, InteractionService interactionService) 
    {
        await interactionService.RegisterCommandsToGuildAsync(185366755277799424UL);
        
        Console.WriteLine($"Commands registered. Logged in as: {client.CurrentUser.Username}");

        Console.WriteLine("Registered slash commands:");
        foreach (var command in interactionService.SlashCommands)
        {
            Console.WriteLine($"/{command.Name} - {command.Description}");
        }
    }
}