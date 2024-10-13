using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace IconSign.Data
{
    public static class RecentIcons
    {
        private const char Delim = '|';

        public static ConfigEntry<string> ConfigEntry;

        private static List<string> Parse()
        {
            return new List<string>(ConfigEntry.Value.Split(Delim));
        }

        internal static IEnumerable<string> Get()
        {
            return Parse();
        }

        internal static void Add(string iconName)
        {
            ConfigEntry.Value = string.Join(
                Delim.ToString(),
                Parse()
                    .Prepend(iconName)
                    .Distinct()
                    .Take(100)
                    .ToArray()
            );
        }
    }
}