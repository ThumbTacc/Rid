using System.Threading.Tasks;
using Discord;

namespace Rid.Services.Moderation
{
    public interface IModerationService
    {
        /// <summary>
        /// Creates a ban in the specified guild.
        /// </summary>
        /// <param name="guild">The guild where the ban is to be created.</param>
        /// <param name="user">The user to be banned.</param>
        /// <param name="executor">The user who executed the command.</param>
        /// <param name="prune">The number of days of messages to be removed.</param>
        /// <param name="reason">The reason for the ban.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        /// <remarks>
        /// Intended for use on a user who is already in the server.
        /// </remarks>
        Task Ban(IGuild guild, IUser user, IUser executor, int prune, string reason);

        /// <summary>
        /// Creates a ban in the specified guild.
        /// </summary>
        /// <param name="guild">The guild where the ban is to be created.</param>
        /// <param name="userId">The Id of the user to be banned.</param>
        /// <param name="executor">The user who executed the command.</param>
        /// <param name="prune">The number of days of messages to be removed.</param>
        /// <param name="reason">The reason for the ban.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        /// <remarks>
        /// Intended for use on a user who is not in the server.
        /// </remarks>
        Task BanForeign(IGuild guild, ulong userId, IUser executor, int prune, string reason);

        /// <summary>
        /// Creates a mute in the specified guild.
        /// </summary>
        /// <param name="guild">The guild where the mute is to be created.</param>
        /// <param name="user">The Id of the user to be muted.</param>
        /// <param name="executor">>The user who executed the command.</param>
        /// <param name="period">The period of the mute.</param>
        /// <param name="reason">The reason for the mute.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        Task Mute(IGuild guild, IUser user, IUser executor, double period, string reason);

        /// <summary>
        /// Creates a mute role in the specified guild.
        /// </summary>
        /// <param name="guild">The guild where the role is to be created.</param>
        /// <returns>
        /// An <see cref="IRole"/>.
        /// </returns>
        /// <remarks>
        /// Specifically created for usage in the Mute command.
        /// </remarks>
        Task<IRole> CreateMuteRole(IGuild guild);
        
        /// <summary>
        /// Kicks a user from the specified guild.
        /// </summary>
        /// <param name="user">The user to be kicked.</param>
        /// <param name="executor">The executor of the command.</param>
        /// <param name="reason">The reason for the kick.</param>
        /// <returns></returns>
        Task Kick(IUser user, IUser executor, string reason);
    }
}