using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Rid.Enums;

namespace Rid.Services.Log
{
    public interface ILogService
    {
        Task<IEnumerable<EmbedFieldBuilder>> CreateLog(IUser user, IUser executor, string reason, Infraction infraction, double period = 10);

        Task SendLog(IMessageChannel channel, IEnumerable<EmbedFieldBuilder> builders);
    }
}