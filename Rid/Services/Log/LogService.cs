using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Rid.Enums;
using Rid.Helpers;

namespace Rid.Services.Log
{
    public class LogService : ILogService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<EmbedFieldBuilder>> CreateLog(IUser user, IUser executor, string reason, Infraction infraction, double period = 10)
        {
            var builders = new List<EmbedFieldBuilder>();

            await Task.Run(() =>
            {
                var builder1 = new EmbedFieldBuilder();
                var builder2 = new EmbedFieldBuilder();
                var builder3 = new EmbedFieldBuilder();
                var builder4 = new EmbedFieldBuilder();

                builder1
                    .WithName("User")
                    .WithValue($"{user.Username}");

                builder2
                    .WithName("Executor")
                    .WithValue($"{executor.Username}");

                builder3
                    .WithName("Reason")
                    .WithValue(reason);

                builder4
                    .WithName("Type")
                    .WithValue(infraction);

                builders.Add(builder1);
                builders.Add(builder2);
                builders.Add(builder3);
                builders.Add(builder4);

                if (infraction is Infraction.Mute)
                {
                    var builder5 = new EmbedFieldBuilder();

                    builder5
                        .WithName("Period")
                        .WithValue(period);

                    builders.Add(builder5);
                }
            });

            return builders;
        }
    }
}