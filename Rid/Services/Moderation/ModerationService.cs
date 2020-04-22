using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Rid.Helpers;

namespace Rid.Services.Moderation
{
    public class ModerationService : IModerationService
    {
        private readonly DiscordSocketClient _client;

        public ModerationService(DiscordSocketClient client)
        {
            _client = client;
        }

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

        public async Task Mute(IUser user, IUser executor, double period, string reason)
        {
            if (executor.IsHigher(user))
            {
                
            }
            else
            {
                
            }
        }
        
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