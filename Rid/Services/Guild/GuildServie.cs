using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;

namespace Rid.Services.Guild
{
    public class GuildServie : IGuildService
    {
        /// <inheritdoc/>
        public async Task CreateRole(IGuild guild, string name, GuildPermissions permission, Color color, bool hoisted, bool mention)
        {
            await guild.CreateRoleAsync(name, permission, color, hoisted, mention);
        }
    }
}