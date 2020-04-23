using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Rid.Data;
using Rid.Enums;
using Rid.Helpers;
using Rid.Services.Log;
using Rid.Services.Moderation;

namespace Rid.Modules
{
    [Name("Moderation")]
    [Summary("Power over the people.")]
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        private readonly IModerationService _moderation;
        private readonly ILogService _log;

        public Moderation(IModerationService moderation, ILogService log)
        {
            _moderation = moderation;
            _log = log;
        }

        [Command("ban")]
        [Summary("Bans a specified user.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Ban(SocketUser user, int prune = 1, string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.Ban(Context.Guild, user, Context.User, prune, reason);
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Ban);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                await channel.SendMessageAsync(embed: Embeds.CreateLogEmbed("Log", builders));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("banf")]
        [Summary("Bans a specified user using their Id.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanForeign(ulong userId, int prune = 1, string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.BanForeign(Context.Guild, userId, Context.User, prune, reason);
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("mute")]
        [Summary("Mutes as specified user.")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Mute(IUser user, double period = 10, string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.Mute(Context.Guild, user, Context.User, period, reason);
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Mute);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                await channel.SendMessageAsync(embed: Embeds.CreateLogEmbed("Log", builders));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("kick")]
        [Summary("Kicks a specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(IUser user, string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.Kick(user, Context.User, reason);
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Kick);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                await channel.SendMessageAsync(embed: Embeds.CreateLogEmbed("Log", builders));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("warn")]
        [Summary("Warns a specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Warn(IUser user, string reason = "No Reason Provided.")
        {
            try
            {
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Warn);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                await channel.SendMessageAsync(embed: Embeds.CreateLogEmbed("Log", builders));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}