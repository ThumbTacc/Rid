using System;
using System.Threading.Tasks;
using Discord.Commands;
using Rid.Helpers;
using Rid.Services.Utility;

namespace Rid.Modules
{
    [Name("Utility")]
    [Summary("Some information.")]
    public class Utility : ModuleBase<SocketCommandContext>
    {
        private readonly IUtilityService _utility;

        public Utility(IUtilityService utility)
        {
            _utility = utility;
        }

        [Command("bot info")]
        [Alias("bot", "bi")]
        [Summary("Displays bot information.")]
        public async Task BotInfo()
        {
            try
            {
                var botInfo = _utility.ListBotInfo();
                await Context.Channel.SendMessageAsync(embed: Embeds.CreateEmbed("Bot Info", botInfo));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("guild info")]
        [Alias("guild", "gi")]
        [Summary("Displays guild information.")]
        public async Task GuildInfo()
        {
            try
            {
                var guildDictionary = _utility.GetGuildInfo(Context.Guild);
                var guildInfo = _utility.ListGuildInfo(guildDictionary);

                var created = Context.Guild.CreatedAt.ToString("f");
                var icon = Context.Guild.IconUrl;
                
                await Context.Channel.SendMessageAsync(embed: Embeds.CreateEmbed($"{Context.Guild.Name} Info", guildInfo, $"Created on: {created}", icon));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}