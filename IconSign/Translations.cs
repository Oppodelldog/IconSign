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
                    { IconSign.TranslationKeyPaintItem, "Paint item" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("German",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Piktrogramm Schild" },
                    { IconSign.TranslationKeyUse, "Bemalen" },
                    { IconSign.TranslationKeyPaintItem, "Objekt Malen" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("French",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Panneau d'icônes" },
                    { IconSign.TranslationKeyUse, "Peindre" },
                    { IconSign.TranslationKeyPaintItem, "Peindre l'objet" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Spanish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Señal de icono" },
                    { IconSign.TranslationKeyUse, "Pintura" },
                    { IconSign.TranslationKeyPaintItem, "Pintar objeto" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Norwegian",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Ikon skilt" },
                    { IconSign.TranslationKeyUse, "Maling" },
                    { IconSign.TranslationKeyPaintItem, "Maling objekt" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Swedish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Ikon skylt" },
                    { IconSign.TranslationKeyUse, "Måla" },
                    { IconSign.TranslationKeyPaintItem, "Måla objekt" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Danish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Ikon skilt" },
                    { IconSign.TranslationKeyUse, "Maling" },
                    { IconSign.TranslationKeyPaintItem, "Maling objekt" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Finnish",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Kuvakekyltti" },
                    { IconSign.TranslationKeyUse, "Maali" },
                    { IconSign.TranslationKeyPaintItem, "Maalaa kohde" },
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation("Italian",
                new Dictionary<string, string>
                {
                    { IconSign.TranslationKeyName, "Segnale icona" },
                    { IconSign.TranslationKeyUse, "Pittura" },
                    { IconSign.TranslationKeyPaintItem, "Pittura oggetto" },
                }
            );
        }
    }
}