using BepInEx;
using IconSign.Data;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;

namespace IconSign
{
    [BepInDependency(Main.ModGuid)]
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    internal class Plugin : BaseUnityPlugin
    {
        // ReSharper disable block MemberCanBePrivate.Global
        public const string PluginGuid = "oppodelldog.mod.iconsign";
        public const string PluginName = "IconSign";
        public const string PluginVersion = "0.2.0";

        private const string BuildPieceName = "iconsign";

        private void Awake()
        {
            RecentIcons.ConfigEntry = Config.Bind("config", "recent_icons", "", "your recently used icons");

            PrefabManager.OnVanillaPrefabsAvailable += CreateIconSign;
        }

        private static void CreateIconSign()
        {
            PrefabManager.OnVanillaPrefabsAvailable -= CreateIconSign;

            Jotunn.Logger.LogInfo("creating icon sign");
            var iconSignPiece = new PieceConfig
            {
                Name = Sign.IconSign.TranslationKeyName,
                PieceTable = "Hammer",
                Category = "Misc"
            };

            iconSignPiece.AddRequirement(new RequirementConfig("Wood", 1, 0, true));
            iconSignPiece.AddRequirement(new RequirementConfig("Coal", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Raspberry", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Blueberries", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Guck", 1));

            Translations.AddToLocalizationManager();

            var customPiece = new CustomPiece(BuildPieceName, "sign", iconSignPiece);
            PieceManager.Instance.AddPiece(customPiece);
            customPiece.PiecePrefab.gameObject.AddComponent<Sign.IconSign>();
        }
    }
}