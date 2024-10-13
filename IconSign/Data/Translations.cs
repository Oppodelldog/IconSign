using System.Collections.Generic;
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
                    { Sign.IconSign.TranslationKeyName, "Icon Sign" },
                    { Sign.IconSign.TranslationKeyUse, "Paint" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Paint item" },

                    { Sign.IconSign.TabNameInventory, "Inventory" },
                    { Sign.IconSign.TabNameRecent, "Recent" },
                    { Sign.IconSign.TabNameCategories, "Categories" },

                    { Sign.IconSign.CategoryArmor, "Armor" },
                    { Sign.IconSign.CategoryBuilding, "Building" },
                    { Sign.IconSign.CategoryConsumables, "Consumables" },
                    { Sign.IconSign.CategoryFarming, "Farming" },
                    { Sign.IconSign.CategoryFurniture, "Furniture" },
                    { Sign.IconSign.CategoryMiscellaneous, "Misc" },
                    { Sign.IconSign.CategoryWeapons, "Weapons" },
                    { Sign.IconSign.CategoryPlunder, "Plunder" },
                    { Sign.IconSign.CategoryAbstract, "Abstract" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(de,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Icon Schild" },
                    { Sign.IconSign.TranslationKeyUse, "bemalen" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Objekt anmalen" },

                    { Sign.IconSign.TabNameInventory, "Inventar" },
                    { Sign.IconSign.TabNameRecent, "Kürzliche" },
                    { Sign.IconSign.TabNameCategories, "Kategorien" },

                    { Sign.IconSign.CategoryArmor, "Rüstung & Kleidung" },
                    { Sign.IconSign.CategoryBuilding, "Baustruktur" },
                    { Sign.IconSign.CategoryConsumables, "Verbrauchsgüter" },
                    { Sign.IconSign.CategoryFarming, "Anbau & Herstellung" },
                    { Sign.IconSign.CategoryFurniture, "Einrichtung" },
                    { Sign.IconSign.CategoryMiscellaneous, "Sonstiges" },
                    { Sign.IconSign.CategoryWeapons, "Waffen & Werkzeuge" },
                    { Sign.IconSign.CategoryPlunder, "Trophäen" },
                    { Sign.IconSign.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(fr,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Icône de panneau" },
                    { Sign.IconSign.TranslationKeyUse, "Peindre" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Peindre l'objet" },

                    { Sign.IconSign.TabNameInventory, "Inventaire" },
                    { Sign.IconSign.TabNameRecent, "Récent" },
                    { Sign.IconSign.TabNameCategories, "Catégories" },

                    { Sign.IconSign.CategoryArmor, "Armure" },
                    { Sign.IconSign.CategoryBuilding, "Bâtiment" },
                    { Sign.IconSign.CategoryConsumables, "Consommables" },
                    { Sign.IconSign.CategoryFarming, "Agriculture" },
                    { Sign.IconSign.CategoryFurniture, "Meubles" },
                    { Sign.IconSign.CategoryMiscellaneous, "Divers" },
                    { Sign.IconSign.CategoryWeapons, "Armes" },
                    { Sign.IconSign.CategoryPlunder, "Butin" },
                    { Sign.IconSign.CategoryAbstract, "Abstrait" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(es,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Icono de señal" },
                    { Sign.IconSign.TranslationKeyUse, "Pintar" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Pintar objeto" },

                    { Sign.IconSign.TabNameInventory, "Inventario" },
                    { Sign.IconSign.TabNameRecent, "Reciente" },
                    { Sign.IconSign.TabNameCategories, "Categorías" },

                    { Sign.IconSign.CategoryArmor, "Armadura" },
                    { Sign.IconSign.CategoryBuilding, "Edificio" },
                    { Sign.IconSign.CategoryConsumables, "Consumibles" },
                    { Sign.IconSign.CategoryFarming, "Agricultura" },
                    { Sign.IconSign.CategoryFurniture, "Muebles" },
                    { Sign.IconSign.CategoryMiscellaneous, "Diverso" },
                    { Sign.IconSign.CategoryWeapons, "Armas" },
                    { Sign.IconSign.CategoryPlunder, "Botín" },
                    { Sign.IconSign.CategoryAbstract, "Abstracto" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(no,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Ikon skilt" },
                    { Sign.IconSign.TranslationKeyUse, "Maling" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Maling objekt" },

                    { Sign.IconSign.TabNameInventory, "Inventar" },
                    { Sign.IconSign.TabNameRecent, "Siste" },
                    { Sign.IconSign.TabNameCategories, "Kategorier" },

                    { Sign.IconSign.CategoryArmor, "Rustning" },
                    { Sign.IconSign.CategoryBuilding, "Bygning" },
                    { Sign.IconSign.CategoryConsumables, "Forbruksvarer" },
                    { Sign.IconSign.CategoryFarming, "Jordbruk" },
                    { Sign.IconSign.CategoryFurniture, "Møbler" },
                    { Sign.IconSign.CategoryMiscellaneous, "Diverse" },
                    { Sign.IconSign.CategoryWeapons, "Våpen" },
                    { Sign.IconSign.CategoryPlunder, "Plyndring" },
                    { Sign.IconSign.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(sv,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Ikon skylt" },
                    { Sign.IconSign.TranslationKeyUse, "Måla" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Måla objekt" },

                    { Sign.IconSign.TabNameInventory, "Inventering" },
                    { Sign.IconSign.TabNameRecent, "Senaste" },
                    { Sign.IconSign.TabNameCategories, "Kategorier" },

                    { Sign.IconSign.CategoryArmor, "Rustning" },
                    { Sign.IconSign.CategoryBuilding, "Byggnad" },
                    { Sign.IconSign.CategoryConsumables, "Förbrukningsvaror" },
                    { Sign.IconSign.CategoryFarming, "Jordbruk" },
                    { Sign.IconSign.CategoryFurniture, "Möbler" },
                    { Sign.IconSign.CategoryMiscellaneous, "Diverse" },
                    { Sign.IconSign.CategoryWeapons, "Vapen" },
                    { Sign.IconSign.CategoryPlunder, "Plundra" },
                    { Sign.IconSign.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(da,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Ikon skilt" },
                    { Sign.IconSign.TranslationKeyUse, "Maling" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Maling objekt" },

                    { Sign.IconSign.TabNameInventory, "Inventar" },
                    { Sign.IconSign.TabNameRecent, "Seneste" },
                    { Sign.IconSign.TabNameCategories, "Kategorier" },

                    { Sign.IconSign.CategoryArmor, "Rustning" },
                    { Sign.IconSign.CategoryBuilding, "Bygning" },
                    { Sign.IconSign.CategoryConsumables, "Forbrugsvarer" },
                    { Sign.IconSign.CategoryFarming, "Landbrug" },
                    { Sign.IconSign.CategoryFurniture, "Møbler" },
                    { Sign.IconSign.CategoryMiscellaneous, "Diverse" },
                    { Sign.IconSign.CategoryWeapons, "Våben" },
                    { Sign.IconSign.CategoryPlunder, "Plyndring" },
                    { Sign.IconSign.CategoryAbstract, "Abstrakt" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(fi,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Kuvakekyltti" },
                    { Sign.IconSign.TranslationKeyUse, "Maali" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Maalaa kohde" },

                    { Sign.IconSign.TabNameInventory, "Inventaario" },
                    { Sign.IconSign.TabNameRecent, "Viimeisin" },
                    { Sign.IconSign.TabNameCategories, "Kategoriat" },

                    { Sign.IconSign.CategoryArmor, "Panssari" },
                    { Sign.IconSign.CategoryBuilding, "Rakennus" },
                    { Sign.IconSign.CategoryConsumables, "Kulutustavarat" },
                    { Sign.IconSign.CategoryFarming, "Maatalous" },
                    { Sign.IconSign.CategoryFurniture, "Huonekalut" },
                    { Sign.IconSign.CategoryMiscellaneous, "Sekalaiset" },
                    { Sign.IconSign.CategoryWeapons, "Aseet" },
                    { Sign.IconSign.CategoryPlunder, "Ryöstö" },
                    { Sign.IconSign.CategoryAbstract, "Abstrakti" }
                }
            );
            LocalizationManager.Instance.GetLocalization().AddTranslation(it,
                new Dictionary<string, string>
                {
                    { Sign.IconSign.TranslationKeyName, "Segnale icona" },
                    { Sign.IconSign.TranslationKeyUse, "Pittura" },
                    { Sign.IconSign.TranslationKeyPaintItem, "Pittura oggetto" },

                    { Sign.IconSign.TabNameInventory, "Inventario" },
                    { Sign.IconSign.TabNameRecent, "Recente" },
                    { Sign.IconSign.TabNameCategories, "Categorie" },

                    { Sign.IconSign.CategoryArmor, "Armatura" },
                    { Sign.IconSign.CategoryBuilding, "Edificio" },
                    { Sign.IconSign.CategoryConsumables, "Consumabili" },
                    { Sign.IconSign.CategoryFarming, "Agricoltura" },
                    { Sign.IconSign.CategoryFurniture, "Mobili" },
                    { Sign.IconSign.CategoryMiscellaneous, "Varie" },
                    { Sign.IconSign.CategoryWeapons, "Armi" },
                    { Sign.IconSign.CategoryPlunder, "Bottino" },
                    { Sign.IconSign.CategoryAbstract, "Astratto" }
                }
            );
        }
    }
}