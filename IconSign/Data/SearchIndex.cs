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
        private static readonly Dictionary<string, List<string>> Index = new Dictionary<string, List<string>>();

        public static void Init()
        {
            _searchStats = new StatsLogger("Search", DevConfig.SeachIndex.LogSearchStatsEvery.Value);
            var start = DateTime.Now;
            Logger.LogInfo("init search index");
            foreach (var kv in IconTranslation.GetTranslations())
            {
                var words = kv.Value.Split(' ');
                foreach (var word in words)
                {
                    var w = word.ToLower();

                    Index.TryGetValue(w, out var values);
                    if (values == null) values = new List<string>();
                    values.Add(kv.Key);

                    Index[w] = values;
                }
            }

            Logger.LogInfo("search index initialized in " + (DateTime.Now - start).TotalMilliseconds + "ms");

            if (DevConfig.SeachIndex.DumpIndexToFile.Value) DumpIndexToFile();
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
            _searchStats.Start();
            var iconNames = new List<string>();
            query = query.ToLower();
            foreach (var kv in Index)
                if (kv.Key.Contains(query))
                    iconNames.AddRange(kv.Value);

            var result = iconNames.Distinct().ToList();

            _searchStats.Done();

            return result.ToArray();
        }
    }
}