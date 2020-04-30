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
        [Alias("b")]
        [Summary("Bans a specified user.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Ban(
            [Summary("The user to be banned.")]
            SocketUser user, 
            [Summary("The number of days of messages to be deleted.\n(Default: 1)")]
            int prune = 1, 
            [Remainder]
            [Summary("The reason for the ban.\n(Default: \"No Reason Provided.\")")]
            string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.Ban(Context.Guild, user, Context.User, prune, reason);
                
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Ban);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                
                await channel.SendMessageAsync(embed: Embeds.CreateEmbed("Log", builders));
                await Context.Message.AddReactionAsync(new Emoji("✅"));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("banf")]
        [Alias("bf")]
        [Summary("Bans a specified user using their Id.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanForeign(
            [Summary("The Id user to be banned.")]
            ulong userId, 
            [Summary("The number of days of messages to be deleted.\n(Default: 1)")]
            int prune = 1, 
            [Remainder]
            [Summary("The reason for the ban.\n(Default: \"No Reason Provided.\")")]
            string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.BanForeign(Context.Guild, userId, Context.User, prune, reason);
                await Context.Message.AddReactionAsync(new Emoji("✅"));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("mute")]
        [Alias("m")]
        [Summary("Mutes as specified user.")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Mute(
            [Summary("The user to be muted.")]
            IUser user, 
            [Summary("The length of the mute in minutes.\n(Default: 10)")]
            double period = 10, 
            [Summary("The mute period span.\n(Default: Minutes)\n(Available: Seconds, Minutes, Hours, Days)")]
            Measure measure = Measure.Minutes,
            [Remainder]
            [Summary("The reason for the mute.\n(Default: \"No Reason Provided.\")")]
            string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.Mute(Context.Guild, user, Context.User, period, reason);
                await _moderation.StartTimer(Context.Guild, user, period, measure);
                
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Mute);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                
                await channel.SendMessageAsync(embed: Embeds.CreateEmbed("Log", builders));
                await Context.Message.AddReactionAsync(new Emoji("✅"));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("kick")]
        [Alias("k")]
        [Summary("Kicks a specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(
            [Summary("The user to be muted.")]
            IUser user, 
            [Remainder]
            [Summary("The reason for the kick.\n(Default: \"No Reason Provided.\")")]
            string reason = "No Reason Provided.")
        {
            try
            {
                await _moderation.Kick(user, Context.User, reason);
                
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Kick);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                
                await channel.SendMessageAsync(embed: Embeds.CreateEmbed("Log", builders));
                await Context.Message.AddReactionAsync(new Emoji("✅"));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("warn")]
        [Alias("w")]
        [Summary("Warns a specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Warn(
            [Summary("The user to be warned.")]
            IUser user, 
            [Remainder]
            [Summary("The reason for the warn.\n(Default: \"No Reason Provided.\")")]
            string reason = "No Reason Provided.")
        {
            try
            {
                var builders = await _log.CreateLog(user, Context.User, reason, Infraction.Warn);
                var channel = Context.Guild.GetChannel(Config.Log) as IMessageChannel;
                
                await channel.SendMessageAsync(embed: Embeds.CreateEmbed("Log", builders));
                await Context.Message.AddReactionAsync(new Emoji("✅"));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}