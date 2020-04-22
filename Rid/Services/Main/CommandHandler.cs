using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Rid.Data;

namespace Rid.Services.Main
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        public CommandHandler(
            DiscordSocketClient client, 
            CommandService commands,
            IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;

            _client.MessageReceived += HandleCommandAsync;
            _commands.CommandExecuted += HandleExecutedAsync;
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            if (!(msg is SocketUserMessage message) || message.Author.IsBot)
            {
                return;
            }
            
            var context = new SocketCommandContext(_client, message);
            var prefix = Config.Prefix;
            var argPos = 0;
            
            if (message.HasStringPrefix(prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                await _commands.ExecuteAsync(context, argPos, _services);
            }
        }

        private static async Task HandleExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}