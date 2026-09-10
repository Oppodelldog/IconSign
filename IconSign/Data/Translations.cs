using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Jotunn.Entities;
using Jotunn.Managers;

namespace IconSign.Data
{
    internal static class Translations
    {
        private const string ModKeyPrefix = "iconsign_";
        private const string ResourcePrefix = "IconSign.Assets.Translations.";
        private static CustomLocalization _localization;

        internal static void Load()
        {
            _localization = LocalizationManager.Instance.GetLocalization();
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                if (!resourceName.StartsWith(ResourcePrefix, StringComparison.Ordinal) ||
                    !resourceName.EndsWith(".json", StringComparison.Ordinal))
                    continue;

                var language = resourceName.Substring(ResourcePrefix.Length,
                    resourceName.Length - ResourcePrefix.Length - ".json".Length);
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        throw new InvalidOperationException("Missing translation resource: " + resourceName);

                    using (var reader = new StreamReader(stream))
                    {
                        var translations = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, string>>(
                            reader.ReadToEnd());
                        foreach (var translation in translations)
                            _localization.AddTranslation(language, GetNamespacedKey(translation.Key), translation.Value);
                    }
                }
            }
        }

        internal static string Translate(string key)
        {
            return _localization.TryTranslate(GetToken(key));
        }

        internal static string GetToken(string key)
        {
            return "$" + GetNamespacedKey(key);
        }

        private static string GetNamespacedKey(string key)
        {
            return ModKeyPrefix + key;
        }
    }
}
