using System;
using System.Collections.Generic;
using IconSign.Helper;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.U2D;
using Logger = Jotunn.Logger;

namespace IconSign.Data
{
    public static class CategorizedIcons
    {
        internal static Dictionary<string, List<Sprite>> PrepareData(string[] categories)
        {
            var startTime = DateTime.Now;
            Logger.LogInfo("Loading icon categories...");
            var sprites = GetSprites();
            var result = BuildCategorizedIcons(categories, sprites);
            Logger.LogInfo($"Loaded icon categories in {(DateTime.Now - startTime).TotalMilliseconds}ms");

            return result;
        }

        private static Dictionary<string, List<Sprite>> BuildCategorizedIcons(string[] categories, Sprite[] sprites)
        {
            var startTime = DateTime.Now;
            Logger.LogInfo("Building categorized icons...");

            var result = InitResult(categories);
            BuildDataIndices(IconCategories.Data, out var categoryByIcon, out var nameOrderByIcon);
            BuildSpritesByCategoryIndex(sprites, categoryByIcon, result);
            SortSprites(result, nameOrderByIcon);

            Logger.LogInfo($"Built categorized icons in {(DateTime.Now - startTime).TotalMilliseconds}ms");

            return result;
        }

        private static Sprite[] GetSprites()
        {
            var startTime = DateTime.Now;
            Logger.LogInfo("Loading icons...");

            var atlas = PrefabManager.Cache.GetPrefab<SpriteAtlas>("IconAtlas");
            var sprites = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sprites);

            Logger.LogInfo($"Loaded {sprites.Length} sprites in {(DateTime.Now - startTime).TotalMilliseconds}ms");

            return sprites;
        }

        private static void SortSprites(Dictionary<string, List<Sprite>> result, Dictionary<string, int> nameOrderByIcon)
        {
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var kv in result)
            {
                var sprites = kv.Value;
                sprites.Sort((a, b) =>
                {
                    var nameA = IconName.GetName(a);
                    var nameB = IconName.GetName(b);
                    return nameOrderByIcon[nameA].CompareTo(nameOrderByIcon[nameB]);
                });
            }
        }

        private static void BuildSpritesByCategoryIndex(Sprite[] sprites, Dictionary<string, string> categoryByIcon, Dictionary<string, List<Sprite>> result)
        {
            foreach (var sprite in sprites)
            {
                var iconName = IconName.GetName(sprite);
                if (categoryByIcon.TryGetValue(iconName, out var category))
                {
                    result[category].Add(sprite);
                }
                else
                {
                    Logger.LogWarning($"Icon {iconName} has no category");
                }
            }
        }

        private static Dictionary<string, List<Sprite>> InitResult(string[] categories)
        {
            var catSpriteDict = new Dictionary<string, List<Sprite>>();
            foreach (var category in categories)
            {
                catSpriteDict[category] = new List<Sprite>();
            }

            return catSpriteDict;
        }

        private static void BuildDataIndices(Dictionary<string, string[]> data, out Dictionary<string, string> categoryByIcon, out Dictionary<string, int> nameOrderByIcon)
        {
            categoryByIcon = new Dictionary<string, string>();
            nameOrderByIcon = new Dictionary<string, int>();
            foreach (var kv in data)
            {
                var category = kv.Key;
                var icons = kv.Value;
                for (var i = 0; i < icons.Length; i++)
                {
                    var icon = icons[i];
                    categoryByIcon[icon] = category;
                    nameOrderByIcon[icon] = i;
                }
            }
        }
    }
}