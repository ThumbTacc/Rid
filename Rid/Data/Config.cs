using Rid.Bot;

namespace Rid.Data
{
    public static class Config
    {
        /// <summary>
        /// The release connection Token.
        /// </summary>
        public static readonly string ReleaseToken = Startup.Configuration["token:release"];
        
        /// <summary>
        /// The debug connection Token.
        /// </summary>
        public static readonly string DebugToken = Startup.Configuration["token:debug"];
        
        /// <summary>
        /// The default command Prefix.
        /// </summary>
        public const string Prefix = ".";
        
        /// <summary>
        /// The moderation log channel Id.
        /// </summary>
        public const ulong Log = 702617427577536562;
    }
}