﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Rid.Data;
using Rid.Enums;
using Rid.Helpers;

namespace Rid.Services.Moderation
{
    /// <summary>
    /// Implements <see cref="IModerationService"/>.
    /// </summary>
    public class ModerationService : IModerationService
    {
        private readonly DiscordSocketClient _client;

        /// <summary>
        /// Constructs a new <see cref="ModerationService"/> with the given injected dependencies.
        /// </summary>
        /// <param name="client">The Discord client."/></param>
        public ModerationService(DiscordSocketClient client)
        {
            _client = client;
        }

        private Timer _timer;

        /// <inheritdoc/>
        public async Task Ban(IGuild guild, IUser user, IUser executor, int prune, string reason)
        {
            if (executor.IsHigher(user))
            {
                await guild.AddBanAsync(user, prune, reason);
            }
            else
            {
                throw new Exception("You cannot ban this user.");
            }
        }

        /// <inheritdoc/>
        public async Task BanForeign(IGuild guild, ulong userId, IUser executor, int prune, string reason)
        {
            var user = _client.GetUser(userId);
            
            if (executor.IsHigher(user))
            {
                await guild.AddBanAsync(user, prune, reason);
            }
            else
            {
                throw new Exception("You cannot ban this user.");
            }
        }
        
        /// <inheritdoc/>
        public async Task Kick(IUser user, IUser executor, string reason)
        {
            if (executor.IsHigher(user))
            {
                await (user as IGuildUser).KickAsync(reason);
            }
            else
            {
                throw new Exception("You cannot mute this user.");
            }
        }

        /// <inheritdoc/>
        public async Task Mute(IGuild guild, IUser user, IUser executor, double period, string reason)
        {
            var status = (user as SocketGuildUser).Roles.FirstOrDefault(r => r.Name == Config.Mute);
            if (executor.IsHigher(user) && status != null)
            {
                var role = await GetOrCreateMuteRole(guild);
                await (user as IGuildUser).AddRoleAsync(role);
            }
            else if (status == null)
            {
                throw new Exception("This user is already muted. Please `?unmute` them first and mute again.");
            }
            else
            {
                throw new Exception("You cannot mute this user.");
            }
        }

        /// <inheritdoc/>
        public async Task StartTimer(IGuild guild, IUser user, double period, Measure measure)
        {
            var timespan = period.ParsePeriod(measure);

            _timer = new Timer(async _ => 
            {
                var role = await GetOrCreateMuteRole(guild);
                await (user as IGuildUser).RemoveRoleAsync(role);
            }, 
                null, timespan, TimeSpan.Zero);
        }
        
        /// <inheritdoc/>
        public async Task<IRole> GetOrCreateMuteRole(IGuild guild)
        {
            var role = guild.Roles.FirstOrDefault(r => r.Name == Config.Mute);
            
            if (role == null)
            {
                await guild.CreateRoleAsync(Config.Mute, new GuildPermissions(sendMessages: false), default, false, false);
                return guild.Roles.First(r => r.Name == Config.Mute);
            }
            
            return role;
        }
    }
}