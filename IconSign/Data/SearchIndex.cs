using System.Collections.Generic;
using System.Linq;
using BepInEx;
using UnityEngine;

namespace IconSign.Data
{
    public abstract class SearchIndex
    {
        public static void Init()
        {
            Jotunn.Logger.LogInfo("init search index");
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

            Jotunn.Logger.LogInfo("search index initialized");

            if (Config.DevConfig.SeachIndex.DumpIndexToFile.Value)
            {
                DumpIndexToFile();
            }
        }

        private static void DumpIndexToFile()
        {
            var language = PlayerPrefs.GetString("language", "en");
            var path = $"{Paths.PluginPath}/search_index_{language}.txt";
            Jotunn.Logger.LogInfo($"dumping search index to {path}");
            System.IO.File.WriteAllLines(path, Index.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}"));
        }

        public static string[] Search(string query)
        {
            Index.TryGetValue(query, out var iconName);
            return iconName == null ? new string[] { } : iconName.ToArray();
        }

        private static readonly Dictionary<string, List<string>> Index = new Dictionary<string, List<string>>();
    }
}