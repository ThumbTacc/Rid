using System.Linq;
using Discord;
using Discord.WebSocket;

namespace Rid.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Determines if the specified first user is positioned higher than the specified second user.
        /// </summary>
        /// <param name="user1">The first user to be compared.</param>
        /// <param name="user2">The second user to be compared.</param>
        /// <returns>
        /// A <see cref="bool"/>.
        /// </returns>
        public static bool IsHigher(this IUser user1, IUser user2)
        {
            return (user1 as SocketGuildUser).Hierarchy > (user2 as SocketGuildUser).Hierarchy;
        }

        /// <summary>
        /// Determines if the specified guild has a role by its name.
        /// </summary>
        /// <param name="guild">The guild where the role is to be looked for.</param>
        /// <param name="role">The name of the role to be looked for.</param>
        /// <returns>
        /// A <see cref="bool"/>.
        /// </returns>
        public static bool HasRole(this IGuild guild, string role)
        {
            return guild.Roles.FirstOrDefault(r => r.Name == role) != null;
        }
    }
}