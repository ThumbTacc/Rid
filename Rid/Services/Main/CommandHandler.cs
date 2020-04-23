using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Rid.Data;

namespace Rid.Services.Main
{
    /// <summary>
    /// Describes a service that handles the incoming <see cref="SocketMessage"/>. If the message is determined to be a command,
    /// it executes the command and handles the post-execution result.
    /// </summary>
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        /// <summary>
        /// Constructs a new <see cref="CommandHandler"/> object with the given injected dependencies.
        /// </summary>
        /// <param name="client">The Discord client.</param>
        /// <param name="commands">The Discord command service.</param>
        /// <param name="services">The application service provider.</param>
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

        /// <summary>
        /// Evaluates an incoming <see cref="SocketMessage"/> and executes the message if it is determined to be a command.
        /// </summary>
        /// <param name="msg">The incoming <see cref="SocketMessage"/>.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        private async Task HandleCommandAsync(SocketMessage msg)
        {
            if (!(msg is SocketUserMessage message) || message.Author.IsBot)
            {
                return;
            }
            
            var context = new SocketCommandContext(_client, message);
            const string prefix = Config.Prefix;
            var argPos = 0;
            
            if (message.HasStringPrefix(prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                await _commands.ExecuteAsync(context, argPos, _services);
            }
        }

        /// <summary>
        /// Handles the post-execution of a regardless of its final execution state.
        /// </summary>
        /// <param name="command">The command that was executed.</param>
        /// <param name="context">The <see cref="ICommandContext"/> of the command.</param>
        /// <param name="result">The result of the command execution.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        private static async Task HandleExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}