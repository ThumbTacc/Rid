using System.Threading.Tasks;

namespace Rid.Bot
{
    /// <summary>
    /// The main class of the bot.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Starts the bot.
        /// </summary>
        /// <param name="args">The constructor argument.</param>
        /// <returns>
        /// A <see cref="Task"/> that returns upon completion.
        /// </returns>
        public static Task Main(string[] args) => Startup.RunAsync(args);
    }
}