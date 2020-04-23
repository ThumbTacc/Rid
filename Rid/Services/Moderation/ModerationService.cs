using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Rid.Helpers;

namespace Rid.Services.Moderation
{
    /// <summary>
    /// Implements <see cref="IModerationService"/>.
    /// </summary>
    public class ModerationService : IModerationService
    {
        private readonly DiscordSocketClient _client;

        /// <summary>
        /// Constructs a new <see cref="ModerationService"/> with the given injected dependencies.
        /// </summary>
        /// <param name="client">The Discord client."/></param>
        public ModerationService(DiscordSocketClient client)
        {
            _client = client;
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
        public async Task Kick(IUser user, IUser executor, string reason)
        {
            if (executor.IsHigher(user))
            {
                await (user as IGuildUser).KickAsync(reason);
            }
            else
            {
                throw new Exception("You cannot mute this user.");
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
            var role = guild.Roles.FirstOrDefault(r => r.Name == "rid-muted.");
            
            if (role == null)
            {
                await guild.CreateRoleAsync("rid-muted", new GuildPermissions(sendMessages: false), default, false, false);
                return guild.Roles.First(r => r.Name == "rid-muted");
            }
            
            return role;
        }
    }
}