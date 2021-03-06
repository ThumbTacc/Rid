﻿using System.Collections.Generic;
using Discord;

namespace Rid.Helpers
{
    /// <summary>
    /// Represents a class of static helper <see cref="Embed"/> methods.
    /// </summary>
    public static class Embeds
    {
        /// <summary>
        /// Represents the Discord "Dark, but not black" brand colour.
        /// </summary>
        private static readonly Color _default = new Color(44, 47, 51);
        
        /// <summary>
        /// Creates a generic <see cref="Embed"/>.
        /// </summary>
        /// <param name="title">The title of the embed.</param>
        /// <param name="fields">The included fields of the embed.</param>
        /// <param name="footer">The footer of the embed.</param>
        /// <param name="url">The thumbnail URL of the embed.</param>
        /// <returns>
        /// An <see cref="Embed"/>.
        /// </returns>
        public static Embed CreateEmbed(string title, IEnumerable<EmbedFieldBuilder> fields, string footer = null, string url = null)
        {
            return new EmbedBuilder()
                .WithColor(_default)
                .WithTitle(title)
                .WithFields(fields)
                .WithFooter(footer)
                .WithThumbnailUrl(url)
                .Build();
        }
    }
}