using Rid.Bot;

namespace Rid.Data
{
    public static class Config
    {
        /// <summary>
        /// The default Prefix.
        /// </summary>
        public static readonly string Prefix = Startup.Configuration["debug:prefix"];
        
        /// <summary>
        /// The connection Token.
        /// </summary>
        public static readonly string Token = Startup.Configuration["debug:token"];

        /// <summary>
        /// The channel Id of the moderation log.
        /// </summary>
        public static readonly ulong Log = ulong.Parse(Startup.Configuration["server:log"]);
    }
}