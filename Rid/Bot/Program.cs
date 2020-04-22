using System;
using System.Threading.Tasks;

namespace Rid.Bot
{
    internal static class Program
    {
        public static Task Main(string[] args) => Startup.RunAsync(args);
    }
}