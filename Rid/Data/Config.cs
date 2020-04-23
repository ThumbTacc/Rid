using Rid.Bot;

namespace Rid.Data
{
    public static class Config
    {
        /// <summary>
        /// The connection Token.
        /// </summary>
        public static readonly string Token = Startup.Configuration["debug:token"];
        
        /// <summary>
        /// The default Prefix.
        /// </summary>
        public const string Prefix = "?";

        /// <summary>
        /// The moderation log channel Id.
        /// </summary>
        public const ulong Log = 702617427577536562;
    }
}