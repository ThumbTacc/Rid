using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Rid.Services.Main
{
    /// <summary>
    /// Describes a service that logs all console activity to a log file.
    /// </summary>
    public class LoggingService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        private string LogDirectory { get; }
        private string LogFile => Path.Combine(LogDirectory, $"{DateTime.UtcNow:yyyy-MM-dd}.txt");
        
        /// <summary>
        /// Creates a new <see cref="LoggingService"/> object with the given injected dependencies.
        /// </summary>
        /// <param name="client">The Discord client.</param>
        /// <param name="commands">The Discord command service.</param>
        public LoggingService(DiscordSocketClient client, CommandService commands)
        {
            LogDirectory = Path.Combine(AppContext.BaseDirectory, "logs");

            _client = client;
            _commands = commands;

            _client.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        /// <summary>
        /// Handles the logging of console information. If the log folder does not exist, it is created. Log information
        /// is stored in this generated file. 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Task OnLogAsync(LogMessage msg)
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            if (!File.Exists(LogFile))
            {
                File.Create(LogFile).Dispose();
            }

            var logText = $"{DateTime.UtcNow:hh:mm:ss} [{msg.Severity}] {msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            File.AppendAllText(LogFile, logText + "\n");
            return Console.Out.WriteLineAsync(logText);
        }
    }
}