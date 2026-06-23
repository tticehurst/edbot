using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using edbot.Commands.Types;

namespace edbot.Commands;

public class SystemInfoCommand : InteractionModuleBase<SocketInteractionContext>
{
    private static readonly HttpClient client = new HttpClient();

    [SlashCommand("systeminfo", "Get information about a system from EDSM")]
    public async Task SystemInfoAsync(
        [Summary(description: "The name of the star system to look up")]
        string systemName)
    {
        await DeferAsync();

        try
        {
            var json = await client.GetStringAsync(
                $"https://www.edsm.net/api-v1/systems?systemName={Uri.EscapeDataString(systemName)}&showInformation=1&showPermit=1&showPrimaryStar=1");

            SystemData? systemData = JsonSerializer.Deserialize<List<SystemData>>(json)!.FirstOrDefault();

            if (systemData == null)
            {
                await FollowupAsync($"No data found for system: **{systemName}**");
                return;
            }

            string allegiance = string.IsNullOrEmpty(systemData.Information?.Allegiance) ? "Independent" : systemData.Information.Allegiance;
            string government = string.IsNullOrEmpty(systemData.Information?.Government) ? "None" : systemData.Information.Government;
            string security = string.IsNullOrEmpty(systemData.Information?.Security) ? "Anarchy" : systemData.Information.Security;
            string faction = string.IsNullOrEmpty(systemData.Information?.Faction) ? "None" : systemData.Information.Faction;
            string factionState = string.IsNullOrEmpty(systemData.Information?.FactionState) ? "None" : systemData.Information.FactionState;
            string economy = string.IsNullOrEmpty(systemData.Information?.Economy) ? "None" : systemData.Information.Economy;
            string secondEconomy = string.IsNullOrEmpty(systemData.Information?.SecondEconomy) ? "None" : systemData.Information.SecondEconomy;
            string populationStr = systemData.Information?.Population > 0 ? systemData.Information.Population.ToString("N0") : "0";

            string permitStatus = systemData.RequirePermit 
                ? $"Yes 🛑 (Requires: {systemData.PermitName})" 
                : "No 🟢";

            string starDetails = systemData.PrimaryStar != null
                ? $"{systemData.PrimaryStar.Type} {(systemData.PrimaryStar.IsScoopable ? "(Fuel Scoopable 🔥)" : "(Not Scoopable 🧊)")}"
                : "Unknown";

            Embed embed = new EmbedBuilder()
                .WithTitle($"🌌 System: {systemData.Name}")
                .WithDescription($"Intelligence telemetry for the **{systemData.Name}** system.")
                .WithColor(Color.Orange)
                
                .AddField("Permit Required", permitStatus, inline: false)
                .AddField("Primary Star", starDetails, inline: false)
                
                .AddField("Allegiance", allegiance, inline: true)
                .AddField("Government", government, inline: true)
                .AddField("Security", security, inline: true)
                
                .AddField("Controlling Faction", faction, inline: false)
                .AddField("Faction State", factionState, inline: true)
                .AddField("Population", populationStr, inline: true)
                
                .AddField("Primary Economy", economy, inline: true)
                .AddField("Secondary Economy", secondEconomy, inline: true)
                
                .WithFooter(footer => footer.WithText("EDSM Database Lookup"))
                .WithCurrentTimestamp()
                .Build();

            await FollowupAsync(embed: embed);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error querying EDSM: {ex.Message}");
            await FollowupAsync("An error occurred while fetching system data from EDSM.");
        }
    }
}
