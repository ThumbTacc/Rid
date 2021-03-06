﻿using System;
using System.Linq;
using Discord;
using Discord.WebSocket;
using Rid.Enums;

namespace Rid.Helpers
{
    /// <summary>
    /// Represents a class of static helper extension methods.
    /// </summary>
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

        /// <summary>
        /// Parses a given period to a <see cref="TimeSpan"/> depending on the specified <see cref="Measure"/>.
        /// </summary>
        /// <param name="period">The period to be parsed.</param>
        /// <param name="measure">The measure the period is to be spanned to.</param>
        /// <returns>
        /// A <see cref="TimeSpan"/> that represents the parsed period.
        /// </returns>
        public static TimeSpan ParsePeriod(this double period, Measure measure)
        {
            return measure switch
            {
                Measure.Days => TimeSpan.FromDays(period),
                Measure.Hours => TimeSpan.FromHours(period),
                Measure.Minutes => TimeSpan.FromMinutes(period),
                _ => TimeSpan.FromSeconds(period)
            };
        }
    }
}