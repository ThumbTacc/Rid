using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Rid.Data;

namespace Rid.Services.Main
{
    /// <summary>
    /// Describes a service that handles the loading of various features of the bot on startup.
    /// </summary>
    public class StartupService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        /// <summary>
        /// Creates a new <see cref="StartupService"/> object with the given injected dependencies.
        /// </summary>
        /// <param name="client">The Discord client.</param>
        /// <param name="commands">The Discord command service.</param>
        /// <param name="services">The application service provider.</param>
        public StartupService(
            DiscordSocketClient client, 
            CommandService commands, 
            IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }
        
        /// <summary>
        /// Starts the bot.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        public async Task StartAsync()
        {
            var token = Config.Token;
                
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new Exception("Bot token missing from _config.yml");
            }
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
    }
}