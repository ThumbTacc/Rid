using System.Threading.Tasks;
using Discord;

namespace Rid.Services.Moderation
{
    public interface IModerationService
    {
        Task Ban(IGuild guild, IUser user, IUser executor, int prune, string reason);

        Task BanForeign(IGuild guild, ulong userId, IUser executor, int prune, string reason);

        Task Kick(IUser user, IUser executor, string reason);
    }
}