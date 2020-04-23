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
        public async Task Commands(string moduleName)
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
    }
}