using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Rid.Enums;

namespace Rid.Services.Moderation
{
    /// <summary>
    /// Describes a service for performing moderation actions.
    /// </summary>
    public interface IModerationService
    {
        /// <summary>
        /// Creates a ban in the specified <see cref="IGuild"/>.
        /// </summary>
        /// <param name="guild">The <see cref="IGuild"/> where the ban is to be created.</param>
        /// <param name="user">The <see cref="IUser"/> to be banned.</param>
        /// <param name="executor">The <see cref="IUser"/> who executed the command.</param>
        /// <param name="prune">The number of days of <see cref="SocketMessage"/> to be removed.</param>
        /// <param name="reason">The reason for the ban.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        /// <remarks>
        /// Intended for use on a user who is already in the guild.
        /// </remarks>
        Task Ban(IGuild guild, IUser user, IUser executor, int prune, string reason);

        /// <summary>
        /// Creates a ban in the specified <see cref="IGuild"/>.
        /// </summary>
        /// <param name="guild">The <see cref="IGuild"/> where the ban is to be created.</param>
        /// <param name="userId">The Id of the <see cref="IUser"/> to be banned.</param>
        /// <param name="executor">The <see cref="IUser"/> who executed the command.</param>
        /// <param name="prune">The number of days of messages to be removed.</param>
        /// <param name="reason">The reason for the ban.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        /// <remarks>
        /// Intended for use on a user who is not in the guild.
        /// </remarks>
        Task BanForeign(IGuild guild, ulong userId, IUser executor, int prune, string reason);
        
        /// <summary>
        /// Kicks a user from the specified guild.
        /// </summary>
        /// <param name="user">The <see cref="IUser"/> to be kicked.</param>
        /// <param name="executor">The <see cref="IUser"/> who executed the command.</param>
        /// <param name="reason">The reason for the kick.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        Task Kick(IUser user, IUser executor, string reason);

        /// <summary>
        /// Creates a mute in the specified <see cref="IGuild"/>.
        /// </summary>
        /// <param name="guild">The <see cref="IGuild"/> where the mute is to be created.</param>
        /// <param name="user">The Id of the <see cref="IUser"/> to be muted.</param>
        /// <param name="executor">The <see cref="IUser"/> who executed the command.</param>
        /// <param name="period">The period of the mute.</param>
        /// <param name="reason">The reason for the mute.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        Task Mute(IGuild guild, IUser user, IUser executor, double period, string reason);

        /// <summary>
        /// Starts a <see cref="Timer"/> that will remove the muted role when it finishes.
        /// </summary>
        /// <param name="period">The period of the mute.</param>
        /// <param name="measure">The <see cref="Measure"/> span.</param>
        /// <param name="user">The muted <see cref="IUser"/>.</param>
        /// <param name="guild">The <see cref="IGuild"/> where the mute exists.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        Task StartTimer(IGuild guild, IUser user, double period, Measure measure);
        
        /// <summary>
        /// Creates a mute role in the specified <see cref="IGuild"/>.
        /// </summary>
        /// <param name="guild">The <see cref="IGuild"/> where the role is to be created.</param>
        /// <returns>
        /// An <see cref="IRole"/> called "rid-muted" to be used as the guild muted role.
        /// </returns>
        /// <remarks>
        /// Intended for use with the mute command.
        /// </remarks>
        Task<IRole> GetOrCreateMuteRole(IGuild guild);
    }
}