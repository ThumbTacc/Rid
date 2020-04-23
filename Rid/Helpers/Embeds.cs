using System.Collections;
using System.Collections.Generic;
using Discord;

namespace Rid.Helpers
{
    public static class Embeds
    {
        public static Embed CreateLogEmbed(string title, IEnumerable<EmbedFieldBuilder> fields)
        {
            return new EmbedBuilder()
                .WithTitle(title)
                .WithFields(fields)
                .Build();
        }
    }
}