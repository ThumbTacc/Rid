using System;
using System.Collections;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

namespace Rid.Services.Utility
{
    public interface IUtilityService
    {
        IEnumerable<EmbedFieldBuilder> ListBotInfo();

        IEnumerable<EmbedFieldBuilder> ListGuildInfo(Dictionary<string, object> info);
        
        Dictionary<string, object> GetGuildInfo(SocketGuild guild);
        
        string GetUptime();

        string GetClientStats();
        
        string GetOperatingSystem();
    }
}