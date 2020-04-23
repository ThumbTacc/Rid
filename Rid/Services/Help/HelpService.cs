using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Commands;

namespace Rid.Services.Help
{
    /// <summary>
    /// Implements <see cref="IHelpService"/>.
    /// </summary>
    public class HelpService : IHelpService
    {
        private readonly CommandService _commands;

        /// <summary>
        /// Constructs a new <see cref="HelpService"/> with the given injected dependencies.
        /// </summary>
        /// <param name="commands">The Discord command service.</param>
        public HelpService(CommandService commands)
        {
            _commands = commands;
        }

        /// <inheritdoc/>
        public IEnumerable<EmbedFieldBuilder> ListModules()
        {
            var builders = new List<EmbedFieldBuilder>();
            var modules = GetAllModuleInfo();

            foreach (var module in modules)
            {
                var builder = new EmbedFieldBuilder();

                builder
                    .WithName(module.Name)
                    .WithValue(module.Summary);
                
                builders.Add(builder);
            }

            return builders;
        }

        /// <inheritdoc/>
        public ModuleInfo GetModuleInfo(string module)
        {
            return _commands.Modules.FirstOrDefault(m => string.Equals(m.Name, module, StringComparison.CurrentCultureIgnoreCase));
        }
        
        /// <inheritdoc/>
        public IEnumerable<ModuleInfo> GetAllModuleInfo()
        {
             return _commands.Modules.Where(m => m.Parent == null);
        }

        /// <inheritdoc/>
        public IEnumerable<EmbedFieldBuilder> ListCommands(ModuleInfo module)
        {
            var commands = new StringBuilder();

            foreach (var command in module.Commands)
            {
                commands.AppendLine(command.Name);
            }
            
            var builders = new List<EmbedFieldBuilder>();
            
            var builder = new EmbedFieldBuilder();

            builder
                .WithName("Commands")
                .WithValue($"```\n{commands}```");
            
            builders.Add(builder);
            
            return builders;
        }
        
        /// <inheritdoc/>
        public CommandInfo GetCommandInfo(string module)
        {
            return _commands.Commands.FirstOrDefault(m => string.Equals(m.Name, module, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}