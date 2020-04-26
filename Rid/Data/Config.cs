using Rid.Bot;

namespace Rid.Data
{
    /// <summary>
    /// A class that represents static variables.
    /// </summary>
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
        /// The moderation log channel Id.
        /// </summary>
        public static readonly ulong Log = ulong.Parse(Startup.Configuration["token:channel"]);
        
        /// <summary>
        /// The default command Prefix.
        /// </summary>
        public const string Prefix = "?";

        /// <summary>
        /// The name of the muted role.
        /// </summary>
        public const string Mute = "rid-muted";
    }
}