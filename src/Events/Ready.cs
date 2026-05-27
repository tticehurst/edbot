// src/Events/Ready.cs
using System;
using System.Threading.Tasks;

namespace edbot.Events;

public static class ReadyEvent {
    public static Task Handler() {
        Console.WriteLine("Bot is logged in and ready!");
        return Task.CompletedTask;
    }
}