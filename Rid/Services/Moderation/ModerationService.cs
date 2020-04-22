using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Rid.Helpers;
using Rid.Services.Guild;

namespace Rid.Services.Moderation
{
    public class ModerationService : IModerationService
    {
        private readonly DiscordSocketClient _client;
        private readonly IGuildService _guild;

        public ModerationService(DiscordSocketClient client, IGuildService guild)
        {
            _client = client;
            _guild = guild;
        }

        /// <inheritdoc/>
        public async Task Ban(IGuild guild, IUser user, IUser executor, int prune, string reason)
        {
            if (executor.IsHigher(user))
            {
                await guild.AddBanAsync(user, prune, reason);
            }
            else
            {
                throw new Exception("You cannot ban this user.");
            }
        }

        /// <inheritdoc/>
        public async Task BanForeign(IGuild guild, ulong userId, IUser executor, int prune, string reason)
        {
            var user = _client.GetUser(userId);
            
            if (executor.IsHigher(user))
            {
                await guild.AddBanAsync(user, prune, reason);
            }
            else
            {
                throw new Exception("You cannot ban this user.");
            }
        }

        /// <inheritdoc/>
        public async Task Mute(IGuild guild, IUser user, IUser executor, double period, string reason)
        {
            if (executor.IsHigher(user))
            {
                var role = await CreateMuteRole(guild);
                await (user as IGuildUser).AddRoleAsync(role);
            }
            else
            {
                throw new Exception("You cannot mute this user.");
            }
        }

        /// <inheritdoc/>
        public async Task<IRole> CreateMuteRole(IGuild guild)
        {
            await _guild.CreateRole(guild, "rid-muted", new GuildPermissions(sendMessages: false), default, false, false);
            return guild.Roles.First(r => r.Name == "rid-muted");
        }
        
        /// <inheritdoc/>
        public async Task Kick(IUser user, IUser executor, string reason)
        {
            if (executor.IsHigher(user))
            {
                await (user as IGuildUser).KickAsync(reason);
            }
            else
            {
                throw new Exception("You cannot ban this user.");
            }
        }
    }
}