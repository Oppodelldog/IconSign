using System.Collections.Generic;

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
                    Index.TryGetValue(word, out var values);
                    if(values == null) values = new List<string>();
                    values.Add(kv.Key);
                    Index[word] = values;
                }
            }
        }
        
        public static string[] Search(string query)
        {
            Index.TryGetValue(query, out var iconName);
            return iconName == null ? new string[]{} : iconName.ToArray();
        }

        private static readonly Dictionary<string, List<string>> Index = new Dictionary<string, List<string>>();
    }
}