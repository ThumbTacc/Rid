using System.Threading.Tasks;
using Discord;

namespace Rid.Services.Guild
{
    public interface IGuildService
    {
        /// <summary>
        /// Creates a role in the specified guild.
        /// </summary>
        /// <param name="guild">The guild where the role is to be created.</param>
        /// <param name="name">The name of the role to be created.</param>
        /// <param name="permission">The permissions of the role to be created.</param>
        /// <param name="color">The color of the role to be created.</param>
        /// <param name="hoisted">Whether the created role should be hoisted.</param>
        /// <param name="mention">Whether the created should be mentionable.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        Task CreateRole(IGuild guild, string name, GuildPermissions permission, Color color, bool hoisted, bool mention);
    }
}