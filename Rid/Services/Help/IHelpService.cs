using System.Collections.Generic;
using Discord;
using Discord.Commands;

namespace Rid.Services.Help
{
    /// <summary>
    /// Describes a service for performing help actions.
    /// </summary>
    public interface IHelpService
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains a list of the bot
        /// modules.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> list of bot modules.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListModules();
        
        /// <summary>
        /// Gets a <see cref="ModuleInfo"/> object from the specified module name if a module by that name exists.
        /// </summary>
        /// <param name="module">The name of the module to be searched for.</param>
        /// <returns>
        /// A <see cref="ModuleInfo"/> of the specified bot module.
        /// </returns>
        ModuleInfo GetModuleInfo(string module);
        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="ModuleInfo"/> of all modules.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of all bot modules.
        /// </returns>
        IEnumerable<ModuleInfo> GetAllModuleInfo();
        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains a list of the commands in
        /// the specified module.
        /// </summary>
        /// <param name="module">The module in which the commands are located.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> the specified module commands.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListCommands(ModuleInfo module);

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains information about the
        /// specified command.
        /// </summary>
        /// <param name="command">The <see cref="CommandInfo"/> whose information is to be listed.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> list of command help.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListCommandHelp(CommandInfo command);
        
        /// <summary>
        /// Gets a <see cref="CommandInfo"/> object from the commands in the specified module if a module by that name exists.
        /// </summary>
        /// <param name="command">The name of the command to be searched for.</param>
        /// <returns>
        /// A <see cref="CommandInfo"/> of the specified module.
        /// </returns>
        CommandInfo GetCommandInfo(string command);

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains general help information.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> list of general command help.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListGeneralHelp();
    }
}