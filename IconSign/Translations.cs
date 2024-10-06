using System.Collections.Generic;
using Jotunn.Managers;

namespace IconSign
{
    internal static class Translations
    {
        internal static void Add()
        {
            LocalizationManager.Instance.GetLocalization().AddTranslation("English",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Icon Sign" },
                    { IconSign.TranslationKeyUse, "Paint" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("German",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Piktrogramm Schild" },
                    { IconSign.TranslationKeyUse, "Bemalen" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("French",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Panneau d'icônes" },
                    { IconSign.TranslationKeyUse, "Peindre" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Spanish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Señal de icono" },
                    { IconSign.TranslationKeyUse, "Pintura" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Norwegian",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Ikon skilt" },
                    { IconSign.TranslationKeyUse, "Maling" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Swedish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Ikon skylt" },
                    { IconSign.TranslationKeyUse, "Måla" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Danish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Ikon skilt" },
                    { IconSign.TranslationKeyUse, "Maling" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Finnish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Kuvakekyltti" },
                    { IconSign.TranslationKeyUse, "Maali" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Italian",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Segnale icona" },
                    { IconSign.TranslationKeyUse, "Pittura" },
                }
            );
        }
    }
}