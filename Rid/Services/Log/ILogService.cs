using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Rid.Enums;

namespace Rid.Services.Log
{
    /// <summary>
    /// Describes a service for performing moderation log actions, within the application, within the context of single incoming request.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Sends a moderation log message to the specified channel.
        /// </summary>
        /// <param name="user">The affected user.</param>
        /// <param name="executor">The command executor</param>
        /// <param name="reason">The reason for the infraction.</param>
        /// <param name="infraction">The type of infraction.</param>
        /// <param name="period">If the infraction was a mute, how long it lasts..</param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/>
        /// </returns>
        Task<IEnumerable<EmbedFieldBuilder>> CreateLog(IUser user, IUser executor, string reason, Infraction infraction, double period = 10);
    }
}