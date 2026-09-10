using System;
using System.IO;
using System.Reflection;
using Jotunn.Entities;
using Jotunn.Managers;

namespace IconSign.Data
{
    internal static class Translations
    {
        private const string ResourcePrefix = "IconSign.Assets.Translations.";
        private static CustomLocalization localization;

        internal static void Load()
        {
            localization = LocalizationManager.Instance.GetLocalization();
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
                        localization.AddJsonFile(language, reader.ReadToEnd());
                }
            }
        }

        internal static string Translate(string key)
        {
            return localization.TryTranslate("$" + key);
        }
    }
}
