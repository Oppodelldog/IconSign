using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using IconSign.Config;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace IconSign.Data
{
    
    public abstract class SearchIndex
    {
        private class SearchStats
        {
            private int TotalSearches { get; set; } = 0;
            private int TotalResults { get; set; } = 0;
            private float AvgSearchTime { get; set; } = 0;
            private float MinSearchTime { get; set; } = float.MaxValue;
            private float MaxSearchTime { get; set; } = 0;

            public void Update(float searchTime, int results)
            {
                TotalSearches++;
                TotalResults += results;
                AvgSearchTime = (AvgSearchTime * (TotalSearches - 1) + searchTime) / TotalSearches;
                MinSearchTime = Mathf.Min(MinSearchTime, searchTime);
                MaxSearchTime = Mathf.Max(MaxSearchTime, searchTime);
            }
            
            public override string ToString()
            {
                return $"Total searches: {TotalSearches}, Total results: {TotalResults}, Avg search time: {AvgSearchTime}ms, Min search time: {MinSearchTime}ms, Max search time: {MaxSearchTime}ms";
            }

            public bool IsTimeToLog(int modulo)
            {
                return TotalSearches % modulo == 0;
            }
        } 
        
        private SearchStats _searchStats = new SearchStats();
        private static readonly Dictionary<string, List<string>> Index = new Dictionary<string, List<string>>();

        public static void Init()
        {
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

        public string[] Search(string query)
        {
            var start = DateTime.Now;
            Logger.LogInfo($"searching for '{query}'");
            var iconNames = new List<string>();
            query = query.ToLower();
            foreach (var kv in Index)
                if (kv.Key.Contains(query))
                    iconNames.AddRange(kv.Value);

            var result = iconNames.Distinct().ToList();

            _searchStats.Update((float)(DateTime.Now - start).TotalMilliseconds, result.Count);
            if (_searchStats.IsTimeToLog(DevConfig.SeachIndex.LogSearchStatsEvery.Value))
                Logger.LogInfo(_searchStats.ToString());

            return result.ToArray();
        }
    }
}