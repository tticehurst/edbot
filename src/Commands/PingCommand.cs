using System.Threading.Tasks;
using Discord.Interactions;

namespace edbot.Commands;

public class PingCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Check whether the bot is online")]
    public async Task PingAsync()
    {
        await RespondAsync("Pong!");
    }
}
