using System;
using System.Security.Permissions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Rid.Helpers;
using Rid.Services.Help;

namespace Rid.Modules
{
    [Name("Help")]
    [Summary("Confused?")]
    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly IHelpService _help;

        public Help(IHelpService help)
        {
            _help = help;
        }

        [Command("modules")]
        [Summary("Lists the bot modules.")]
        public async Task Modules()
        {
            try
            {
                var modules = _help.ListModules();
                await Context.Channel.SendMessageAsync(embed: Embeds.CreateEmbed("Modules", modules));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("commands")]
        [Summary("Lists a specified module's commands.")]
        public async Task Commands(
            [Summary("The name of the module to be searched for.")]
            string moduleName)
        {
            try
            {
                var module = _help.GetModuleInfo(moduleName);
                var commands = _help.ListCommands(module);
                await Context.Channel.SendMessageAsync(embed: Embeds.CreateEmbed(module.Name, commands));
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("help")]
        [Alias("h")]
        [Summary("Lists a specified command's help.")]
        public async Task CommandHelp(
            [Summary("The name of the command to be searched for.")]
            string commandName = null)
        {
            try
            {
                if (commandName != null)
                {
                    var command = _help.GetCommandInfo(commandName);
                    var info = _help.ListCommandHelp(command);
                    await Context.Channel.SendMessageAsync(embed: Embeds.CreateEmbed(command.Name, info));
                }
                else
                {
                    var info = _help.ListGeneralHelp();
                    await Context.Channel.SendMessageAsync(embed: Embeds.CreateEmbed("Help", info));
                }
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}