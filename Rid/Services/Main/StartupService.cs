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
    public class StartupService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly IConfigurationRoot _config;

        public StartupService(
            DiscordSocketClient client, 
            CommandService commands, 
            IServiceProvider services, 
            IConfigurationRoot config)
        {
            _client = client;
            _commands = commands;
            _services = services;
            _config = config;
        }
        
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