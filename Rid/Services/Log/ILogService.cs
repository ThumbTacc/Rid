using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Rid.Enums;

namespace Rid.Services.Log
{
    /// <summary>
    /// Describes a service for performing moderation log actions.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains the moderation log
        /// contents.
        /// </summary>
        /// <param name="user">The affected <see cref="IUser"/>.</param>
        /// <param name="executor">The <see cref="IUser"/> who executed the command.</param>
        /// <param name="reason">The reason for the infraction.</param>
        /// <param name="infraction">The type of <see cref="Infraction"/>.</param>
        /// <param name="period">If the <see cref="Infraction"/> was a mute, how long it lasts.</param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/> of moderation log message content.
        /// </returns>
        Task<IEnumerable<EmbedFieldBuilder>> CreateLog(IUser user, IUser executor, string reason, Infraction infraction, double period = 10);
    }
}