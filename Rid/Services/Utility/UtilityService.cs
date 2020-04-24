using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.WebSocket;
using Rid.Services.Main;

namespace Rid.Services.Utility
{
    /// <summary>
    /// Implements <see cref="IUtilityService"/>.
    /// </summary>
    public class UtilityService : IUtilityService
    {
        private readonly DiscordSocketClient _client;

        /// <summary>
        /// Constructs a new <see cref="UtilityService"/> with the given injected dependencies.
        /// </summary>
        /// <param name="client">The Discord client.</param>
        public UtilityService(DiscordSocketClient client)
        {
            _client = client;
        }

        private const string Developer = "Uchuu#9609";
        private const string Library = "Discord.Net 2.2.0";
        private const string Version = "Rid Developer";

        /// <inheritdoc/>
        public IEnumerable<EmbedFieldBuilder> ListBotInfo()
        {
            var builders = new List<EmbedFieldBuilder>();
            
            var builder1 = new EmbedFieldBuilder();
            var builder2 = new EmbedFieldBuilder();
            var builder3 = new EmbedFieldBuilder();
            var builder4 = new EmbedFieldBuilder();
            var builder5 = new EmbedFieldBuilder();
            var builder6 = new EmbedFieldBuilder();

            builder1
                .WithName("Developer")
                .WithValue(Developer)
                .WithIsInline(true);

            builder2
                .WithName("Library")
                .WithValue(Library)
                .WithIsInline(true);

            builder3
                .WithName("Version")
                .WithValue(Version)
                .WithIsInline(true);

            builder4
                .WithName("Uptime")
                .WithValue(GetUptime())
                .WithIsInline(true);

            builder5
                .WithName("Client")
                .WithValue(GetClientStats())
                .WithIsInline(true);

            builder6
                .WithName("OS")
                .WithValue(GetOperatingSystem())
                .WithIsInline(true);

            builders.Add(builder1);
            builders.Add(builder2);
            builders.Add(builder3);
            builders.Add(builder4);
            builders.Add(builder5);
            builders.Add(builder6);

            return builders;
        }

        /// <inheritdoc/>
        public IEnumerable<EmbedFieldBuilder> ListGuildInfo(Dictionary<string, object> info)
        {
            var builders = new List<EmbedFieldBuilder>();

            foreach (var (key, value) in info)
            {
                var builder = new EmbedFieldBuilder();

                builder
                    .WithName(key)
                    .WithValue(value)
                    .WithIsInline(true);
                
                builders.Add(builder);
            }

            return builders;
        }

        /// <inheritdoc/>
        public Dictionary<string, object> GetGuildInfo(SocketGuild guild)
        {
            return new Dictionary<string, object>
            {
                { "Owner", $"{guild.Owner.Username}#{guild.Owner.Discriminator}" },
                { "MFA", guild.MfaLevel },
                { "Verification", guild.VerificationLevel},
                { "Users", guild.MemberCount },
                { "Text Channels", guild.TextChannels.Count },
                { "Voice Channels", guild.VoiceChannels.Count }
            };
        }
        
        /// <inheritdoc/>
        public string GetUptime()
        {
            var days = StartupService.Stopwatch.Elapsed.Days;
            var hours = StartupService.Stopwatch.Elapsed.Hours;
            var minutes = StartupService.Stopwatch.Elapsed.Minutes;
            var seconds = StartupService.Stopwatch.Elapsed.Seconds;
            
            var builder = new StringBuilder();

            builder
                .AppendLine($"{days} Days")
                .AppendLine($"{hours} Hours")
                .AppendLine($"{minutes} Minutes")
                .AppendLine($"{seconds} Seconds");

            return builder.ToString();
        }

        /// <inheritdoc/>
        public string GetClientStats()
        {
            var guilds = _client.Guilds.Count;
            var users = _client.Guilds.Select(g => g.MemberCount).Sum();

            var builder = new StringBuilder();

            builder
                .AppendLine(guilds == 1 ? $"{guilds.ToString()} Guild" : $"{guilds.ToString()} Guilds")
                .AppendLine($"{users.ToString()} Users");

            return builder.ToString();
        }
        
        /// <inheritdoc/>
        public string GetOperatingSystem()
        {
            return Environment.OSVersion.VersionString.Contains("NT") ? $"Windows\n{Environment.OSVersion.Version}" : Environment.OSVersion.VersionString;
        }
    }
}