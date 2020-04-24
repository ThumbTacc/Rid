using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rid.Data;
using Rid.Services.Help;
using Rid.Services.Log;
using Rid.Services.Main;
using Rid.Services.Moderation;
using Rid.Services.Utility;

namespace Rid.Bot
{
    public class Startup
    {
        /// <summary>
        /// The static <see cref="IConfigurationRoot"/> instance used in <see cref="Config"/>.
        /// </summary>
        public static IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// Creates a new <see cref="IConfigurationRoot"/> object that contains bot configuration data.
        /// </summary>
        /// <param name="args">The constructor argument.</param>
        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("_config.json");
            
            Configuration = builder.Build();
        }
        
        /// <summary>
        /// Runs the bot application.
        /// </summary>
        /// <param name="args">The constructor argument.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="IServiceCollection"/> and builds the service provider.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        private async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<LoggingService>();
            provider.GetRequiredService<CommandHandler>();

            provider.GetRequiredService<IModerationService>();
            provider.GetRequiredService<ILogService>();
            provider.GetRequiredService<IHelpService>();
            provider.GetRequiredService<IUtilityService>();

            await provider.GetRequiredService<StartupService>().StartAsync();
            await Task.Delay(-1);
        }
        
        /// <summary>
        /// Configures the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async
                }))
                .AddSingleton<CommandHandler>()
                .AddSingleton<StartupService>()
                .AddSingleton<LoggingService>()

                .AddScoped<IModerationService, ModerationService>()
                .AddScoped<ILogService, LogService>()
                .AddScoped<IHelpService, HelpService>()
                .AddScoped<IUtilityService, UtilityService>()
                
                .AddSingleton(Configuration)
                .AddScoped<Stopwatch>()
                .AddScoped<Timer>();
        }
    }
}