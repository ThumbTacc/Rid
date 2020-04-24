using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

namespace Rid.Services.Utility
{
    /// <summary>
    /// Describes a service for performing utility actions.
    /// </summary>
    public interface IUtilityService
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains a list of bot
        /// information.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> list of bot information.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListBotInfo();

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains a list of guild
        /// information.
        /// </summary>
        /// <param name="info">The <see cref="string"/> of guild info, taken from the
        /// <see cref="GetGuildInfo"/> method.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> list of guild information.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListGuildInfo(Dictionary<string, object> info);
        
        /// <summary>
        /// Gets a <see cref="Dictionary{TKey,TValue}"/> that contains various information about the
        /// context <see cref="IGuild"/>
        /// </summary>
        /// <param name="guild">The context guild where the information is taken from.</param>
        /// <returns>
        /// Information about the context guild.
        /// </returns>
        Dictionary<string, object> GetGuildInfo(SocketGuild guild);
        
        /// <summary>
        /// Gets the <see cref="TimeSpan"/> as days, hours, minutes, and seconds since the application started.
        /// </summary>
        /// <returns>
        /// The time since the application started.
        /// </returns>
        string GetUptime();

        /// <summary>
        /// Gets various client information and spastics.
        /// </summary>
        /// <returns>
        /// Client statistics.
        /// </returns>
        string GetClientStats();
        
        /// <summary>
        /// Gets the current operating system as a formatted string.
        /// </summary>
        /// <returns>
        /// The current operating system.
        /// </returns>
        string GetOperatingSystem();
    }
}