using System.Linq;
using Discord;
using Discord.WebSocket;

namespace Rid.Helpers
{
    public static class Extensions
    {
        public static bool IsHigher(this IUser user1, IUser user2)
        {
            return (user1 as SocketGuildUser).Hierarchy > (user2 as SocketGuildUser).Hierarchy;
        }

        public static bool HasRole(this IGuild guild, string role)
        {
            return guild.Roles.FirstOrDefault(r => r.Name == role) != null;
        }
    }
}