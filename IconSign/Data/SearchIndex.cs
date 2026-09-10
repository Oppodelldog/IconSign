using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using IconSign.Config;
using IconSign.Helper;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace IconSign.Data
{
    public abstract class SearchIndex
    {
        private static StatsLogger _searchStats;
        private static readonly Dictionary<string, HashSet<string>> Index = new Dictionary<string, HashSet<string>>();

        public static void Init()
        {
            Index.Clear();
            _searchStats = new StatsLogger("Search", DevConfig.SeachIndex.LogSearchStatsEvery.Value);
            var start = DateTime.Now;
            Logger.LogInfo("init search index");
            foreach (var kv in IconTranslation.GetTranslations())
            {
                AddIconName(kv.Key);
                AddTranslation(kv.Key, kv.Value);
            }

            foreach (var iconName in IconCategories.Data.Values.SelectMany(x => x).Distinct())
            {
                AddIconName(iconName);
            }

            Logger.LogInfo("search index initialized in " + (DateTime.Now - start).TotalMilliseconds + "ms");

            if (DevConfig.SeachIndex.DumpIndexToFile.Value) DumpIndexToFile();
        }

        internal static void AddIconName(string iconName)
        {
            AddToIndex(iconName, iconName);
        }

        private static void AddTranslation(string iconName, string translation)
        {
            if (string.IsNullOrEmpty(translation)) return;

            foreach (var word in translation.Split((char[])null, StringSplitOptions.RemoveEmptyEntries))
            {
                AddToIndex(word, iconName);
            }
        }

        private static void AddToIndex(string searchTerm, string iconName)
        {
            if (string.IsNullOrEmpty(searchTerm)) return;

            var key = searchTerm.ToLowerInvariant();
            Index.TryGetValue(key, out var values);
            if (values == null) values = new HashSet<string>(StringComparer.Ordinal);
            values.Add(iconName);

            Index[key] = values;
        }

        private static void DumpIndexToFile()
        {
            var language = PlayerPrefs.GetString("language", "en");
            var path = $"{Paths.PluginPath}/search_index_{language}.txt";
            Logger.LogInfo($"dumping search index to {path}");
            File.WriteAllLines(path, Index.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}"));
        }

        public static string[] Search(string query)
        {
            _searchStats?.Start();
            try
            {
                var terms = (query ?? string.Empty).ToLowerInvariant()
                    .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                    .Distinct();
                HashSet<string> matches = null;

                foreach (var term in terms)
                {
                    var termMatches = new HashSet<string>(StringComparer.Ordinal);
                    foreach (var entry in Index)
                        if (entry.Key.Contains(term))
                            termMatches.UnionWith(entry.Value);

                    if (matches == null)
                        matches = termMatches;
                    else
                        matches.IntersectWith(termMatches);

                    if (matches.Count == 0) break;
                }

                return matches == null
                    ? Index.Values.SelectMany(names => names).Distinct().ToArray()
                    : matches.ToArray();
            }
            finally
            {
                _searchStats?.Done();
            }
        }
    }
}
