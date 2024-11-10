using System.Collections.Generic;
using System.Linq;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace IconSign.Data
{
    public abstract class IconTranslation
    {
        private static readonly Dictionary<string, string> Translations = new Dictionary<string, string>();

        public static void Register()
        {
            PieceManager.OnPiecesRegistered += Init;
        }

        private static void Init()
        {
            Logger.LogInfo("init translations");

            InitFromPrefabs();
            InitFromPieces();

            Logger.LogInfo($"{Translations.Count} translation load");

            SearchIndex.Init();
        }

        public static string Translate(string iconName)
        {
            if (Translations.Count == 0) Init();
            Translations.TryGetValue(iconName, out var translation);
            return translation;
        }

        private static void InitFromPrefabs()
        {
            var allPrefabs = new List<GameObject>();
            allPrefabs.AddRange(ZNetScene.instance.m_nonNetViewPrefabs);
            allPrefabs.AddRange(ZNetScene.instance.m_prefabs);

            allPrefabs.RemoveAll(x => CustomPrefab.IsCustomPrefab(x.name));
            allPrefabs = allPrefabs.OrderBy(x => x.name).ToList();

            foreach (var prefab in allPrefabs)
            {
                var drop = prefab.GetComponent<ItemDrop>();
                if (drop == null) continue;
                if (drop.m_itemData == null) continue;
                if (drop.m_itemData.m_shared.m_icons.Length <= 0) continue;

                var iconName = drop.m_itemData.m_shared.m_icons[0].name;

                if (Translations.ContainsKey(iconName)) continue;
                if (!drop.m_itemData.m_shared.m_name.StartsWith("$")) continue;

                Translations.Add(iconName, LocalizationManager.Instance.TryTranslate(drop.m_itemData.m_shared.m_name));
            }
        }

        private static void InitFromPieces()
        {
            foreach (var table in PieceManager.Instance.GetPieceTables())
            foreach (var piece in table.m_pieces)
            {
                var p = piece.GetComponent<Piece>();
                if (Translations.ContainsKey(p.m_icon.name)) continue;
                if (!p.m_name.StartsWith("$")) continue;

                Translations.Add(p.m_icon.name, LocalizationManager.Instance.TryTranslate(p.m_name));
            }
        }

        public static Dictionary<string, string> GetTranslations()
        {
            return Translations;
        }
    }
}