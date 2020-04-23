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
        /// An <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListModules();
        
        /// <summary>
        /// Gets a <see cref="ModuleInfo"/> object from the specified module name if a module by that name exists.
        /// </summary>
        /// <param name="module">The name of the module to be searched for.</param>
        /// <returns>
        /// A <see cref="ModuleInfo"/>.
        /// </returns>
        ModuleInfo GetModuleInfo(string module);
        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="ModuleInfo"/> of all modules.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<ModuleInfo> GetAllModuleInfo();
        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="EmbedFieldBuilder"/> that contains a list of the commands in
        /// the specified module.
        /// </summary>
        /// <param name="module">The module in which the commands are located.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<EmbedFieldBuilder> ListCommands(ModuleInfo module);
        
        /// <summary>
        /// Gets a <see cref="CommandInfo"/> object from the commands in the specified module if a module by that name exists.
        /// </summary>
        /// <param name="module">The name of the module to be searched for.</param>
        /// <returns>
        /// A <see cref="CommandInfo"/>.
        /// </returns>
        CommandInfo GetCommandInfo(string module);
    }
}