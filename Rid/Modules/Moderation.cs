using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Rid.Services.Moderation;

namespace Rid.Modules
{
    [Name("Moderation")]
    [Summary("Power over the people.")]
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        private readonly IModerationService _moderation;

        public Moderation(IModerationService moderation)
        {
            _moderation = moderation;
        }

        [Command("ban")]
        [Summary("Bans a specified user.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Ban(SocketUser user, int prune = 1, string reason = "No Reason Provided")
        {
            try
            {
                await _moderation.Ban(Context.Guild, user, Context.User, prune, reason);
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}