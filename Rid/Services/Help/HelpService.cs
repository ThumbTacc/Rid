using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Commands;
using Rid.Data;

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

        public IEnumerable<EmbedFieldBuilder> ListCommandHelp(CommandInfo command)
        {
            var builders = new List<EmbedFieldBuilder>();
            
            var builder1 = new EmbedFieldBuilder();
            var builder2 = new EmbedFieldBuilder();
            
            var usage = new StringBuilder();
            
            usage.Append($"{command.Name} ");

            foreach (var parameter in command.Parameters)
            {
                usage.Append($"[{parameter.Name }] ");
            }
            
            builder2
                .WithName("Usage")
                .WithValue(usage);
            
            builders.Add(builder2);
            
            var aliases = new StringBuilder();
            
            if (command.Aliases.Count != 1)
            {
                foreach (var alias in command.Aliases)
                {
                    aliases.Append($"{alias}, ");
                }
            }
            else
            {
                aliases.Append("None");
            }

            builder1
                .WithName("Aliases")
                .WithValue(aliases);
            
            
            foreach (var parameter in command.Parameters)
            {
                var builder3 = new EmbedFieldBuilder();

                builder3
                    .WithName(parameter.Name)
                    .WithValue($"\n{parameter.Summary}");
                
                builders.Add(builder3);
            }
            
            return builders;
        }
        
        /// <inheritdoc/>
        public CommandInfo GetCommandInfo(string command)
        {
            return _commands.Commands.FirstOrDefault(c => string.Equals(c.Name, command, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<EmbedFieldBuilder> ListGeneralHelp()
        {
            var builders = new List<EmbedFieldBuilder>();
            
            var builder1 = new EmbedFieldBuilder();
            var builder2 = new EmbedFieldBuilder();
            var builder3 = new EmbedFieldBuilder();

            const string prefix = Config.Prefix;
            
            builder1
                .WithName($"{prefix}modules")
                .WithValue("To see a list of command modules.");

            builder2
                .WithName($"{prefix}commands [module]")
                .WithValue("To see a list of commands within a module.");

            builder3
                .WithName($"{prefix}help [command]")
                .WithValue("To see more information about a command.");
            
            builders.Add(builder1);
            builders.Add(builder2);
            builders.Add(builder3);

            return builders;
        }
    }
}