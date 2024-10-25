using System.Collections.Generic;
using IconSign.Config;
using Jotunn.Managers;

// ReSharper disable InconsistentNaming
namespace IconSign.Data
{
    internal static class Translations
    {
        private const string en = "English";
        private const string de = "German";
        private const string fr = "French";
        private const string es = "Spanish";
        private const string no = "Norwegian";
        private const string sv = "Swedish";
        private const string da = "Danish";
        private const string fi = "Finnish";
        private const string it = "Italian";

        internal static void AddToLocalizationManager()
        {
            LocalizationManager.Instance.GetLocalization().AddTranslation(en,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Icon Sign" },
                    { Constants.TranslationKeyUse, "Paint" },
                    { Constants.TranslationKeyPaintItem, "Paint item" },

                    { Constants.TabNameInventory, "Inventory" },
                    { Constants.TabNameRecent, "Recent" },
                    { Constants.TabNameCategories, "Categories" },

                    { Constants.CategoryArmor, "Armor" },
                    { Constants.CategoryBuilding, "Building" },
                    { Constants.CategoryConsumables, "Consumables" },
                    { Constants.CategoryFarming, "Farming" },
                    { Constants.CategoryFurniture, "Furniture" },
                    { Constants.CategoryMiscellaneous, "Misc" },
                    { Constants.CategoryWeapons, "Weapons" },
                    { Constants.CategoryPlunder, "Plunder" },
                    { Constants.CategoryAbstract, "Abstract" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(de,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Icon Schild" },
                    { Constants.TranslationKeyUse, "bemalen" },
                    { Constants.TranslationKeyPaintItem, "Objekt anmalen" },

                    { Constants.TabNameInventory, "Inventar" },
                    { Constants.TabNameRecent, "Kürzliche" },
                    { Constants.TabNameCategories, "Kategorien" },

                    { Constants.CategoryArmor, "Rüstung & Kleidung" },
                    { Constants.CategoryBuilding, "Baustruktur" },
                    { Constants.CategoryConsumables, "Verbrauchsgüter" },
                    { Constants.CategoryFarming, "Anbau & Herstellung" },
                    { Constants.CategoryFurniture, "Einrichtung" },
                    { Constants.CategoryMiscellaneous, "Sonstiges" },
                    { Constants.CategoryWeapons, "Waffen & Werkzeuge" },
                    { Constants.CategoryPlunder, "Trophäen" },
                    { Constants.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(fr,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Icône de panneau" },
                    { Constants.TranslationKeyUse, "Peindre" },
                    { Constants.TranslationKeyPaintItem, "Peindre l'objet" },

                    { Constants.TabNameInventory, "Inventaire" },
                    { Constants.TabNameRecent, "Récent" },
                    { Constants.TabNameCategories, "Catégories" },

                    { Constants.CategoryArmor, "Armure" },
                    { Constants.CategoryBuilding, "Bâtiment" },
                    { Constants.CategoryConsumables, "Consommables" },
                    { Constants.CategoryFarming, "Agriculture" },
                    { Constants.CategoryFurniture, "Meubles" },
                    { Constants.CategoryMiscellaneous, "Divers" },
                    { Constants.CategoryWeapons, "Armes" },
                    { Constants.CategoryPlunder, "Butin" },
                    { Constants.CategoryAbstract, "Abstrait" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(es,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Icono de señal" },
                    { Constants.TranslationKeyUse, "Pintar" },
                    { Constants.TranslationKeyPaintItem, "Pintar objeto" },

                    { Constants.TabNameInventory, "Inventario" },
                    { Constants.TabNameRecent, "Reciente" },
                    { Constants.TabNameCategories, "Categorías" },

                    { Constants.CategoryArmor, "Armadura" },
                    { Constants.CategoryBuilding, "Edificio" },
                    { Constants.CategoryConsumables, "Consumibles" },
                    { Constants.CategoryFarming, "Agricultura" },
                    { Constants.CategoryFurniture, "Muebles" },
                    { Constants.CategoryMiscellaneous, "Diverso" },
                    { Constants.CategoryWeapons, "Armas" },
                    { Constants.CategoryPlunder, "Botín" },
                    { Constants.CategoryAbstract, "Abstracto" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(no,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Ikon skilt" },
                    { Constants.TranslationKeyUse, "Maling" },
                    { Constants.TranslationKeyPaintItem, "Maling objekt" },

                    { Constants.TabNameInventory, "Inventar" },
                    { Constants.TabNameRecent, "Siste" },
                    { Constants.TabNameCategories, "Kategorier" },

                    { Constants.CategoryArmor, "Rustning" },
                    { Constants.CategoryBuilding, "Bygning" },
                    { Constants.CategoryConsumables, "Forbruksvarer" },
                    { Constants.CategoryFarming, "Jordbruk" },
                    { Constants.CategoryFurniture, "Møbler" },
                    { Constants.CategoryMiscellaneous, "Diverse" },
                    { Constants.CategoryWeapons, "Våpen" },
                    { Constants.CategoryPlunder, "Plyndring" },
                    { Constants.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(sv,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Ikon skylt" },
                    { Constants.TranslationKeyUse, "Måla" },
                    { Constants.TranslationKeyPaintItem, "Måla objekt" },

                    { Constants.TabNameInventory, "Inventering" },
                    { Constants.TabNameRecent, "Senaste" },
                    { Constants.TabNameCategories, "Kategorier" },

                    { Constants.CategoryArmor, "Rustning" },
                    { Constants.CategoryBuilding, "Byggnad" },
                    { Constants.CategoryConsumables, "Förbrukningsvaror" },
                    { Constants.CategoryFarming, "Jordbruk" },
                    { Constants.CategoryFurniture, "Möbler" },
                    { Constants.CategoryMiscellaneous, "Diverse" },
                    { Constants.CategoryWeapons, "Vapen" },
                    { Constants.CategoryPlunder, "Plundra" },
                    { Constants.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(da,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Ikon skilt" },
                    { Constants.TranslationKeyUse, "Maling" },
                    { Constants.TranslationKeyPaintItem, "Maling objekt" },

                    { Constants.TabNameInventory, "Inventar" },
                    { Constants.TabNameRecent, "Seneste" },
                    { Constants.TabNameCategories, "Kategorier" },

                    { Constants.CategoryArmor, "Rustning" },
                    { Constants.CategoryBuilding, "Bygning" },
                    { Constants.CategoryConsumables, "Forbrugsvarer" },
                    { Constants.CategoryFarming, "Landbrug" },
                    { Constants.CategoryFurniture, "Møbler" },
                    { Constants.CategoryMiscellaneous, "Diverse" },
                    { Constants.CategoryWeapons, "Våben" },
                    { Constants.CategoryPlunder, "Plyndring" },
                    { Constants.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(fi,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Kuvakekyltti" },
                    { Constants.TranslationKeyUse, "Maali" },
                    { Constants.TranslationKeyPaintItem, "Maalaa kohde" },

                    { Constants.TabNameInventory, "Inventaario" },
                    { Constants.TabNameRecent, "Viimeisin" },
                    { Constants.TabNameCategories, "Kategoriat" },

                    { Constants.CategoryArmor, "Panssari" },
                    { Constants.CategoryBuilding, "Rakennus" },
                    { Constants.CategoryConsumables, "Kulutustavarat" },
                    { Constants.CategoryFarming, "Maatalous" },
                    { Constants.CategoryFurniture, "Huonekalut" },
                    { Constants.CategoryMiscellaneous, "Sekalaiset" },
                    { Constants.CategoryWeapons, "Aseet" },
                    { Constants.CategoryPlunder, "Ryöstö" },
                    { Constants.CategoryAbstract, "Abstrakti" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(it,
                new Dictionary<string, string>
                {
                    { Constants.TranslationKeyName, "Segnale icona" },
                    { Constants.TranslationKeyUse, "Pittura" },
                    { Constants.TranslationKeyPaintItem, "Pittura oggetto" },

                    { Constants.TabNameInventory, "Inventario" },
                    { Constants.TabNameRecent, "Recente" },
                    { Constants.TabNameCategories, "Categorie" },

                    { Constants.CategoryArmor, "Armatura" },
                    { Constants.CategoryBuilding, "Edificio" },
                    { Constants.CategoryConsumables, "Consumabili" },
                    { Constants.CategoryFarming, "Agricoltura" },
                    { Constants.CategoryFurniture, "Mobili" },
                    { Constants.CategoryMiscellaneous, "Varie" },
                    { Constants.CategoryWeapons, "Armi" },
                    { Constants.CategoryPlunder, "Bottino" },
                    { Constants.CategoryAbstract, "Astratto" }
                }
            );
        }
    }
}