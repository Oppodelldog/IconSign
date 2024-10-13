using IconSign.Assets;
using IconSign.Config;
using IconSign.Data;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace IconSign.Sign
{
    public static class IconSignFactory
    {
        private const string BuildPieceName = "iconsign";


        internal static void Register()
        {
            PrefabManager.OnVanillaPrefabsAvailable += CreateIconSign;
        }

        private static void CreateIconSign()
        {
            PrefabManager.OnVanillaPrefabsAvailable -= CreateIconSign;

            Logger.LogInfo("creating icon sign");

            var iconSignPiece = new PieceConfig
            {
                Name = Constants.TranslationKeyName,
                PieceTable = "Hammer",
                Category = "Misc"
            };

            iconSignPiece.AddRequirement(new RequirementConfig("Wood", 1, 0, true));
            iconSignPiece.AddRequirement(new RequirementConfig("Coal", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Raspberry", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Blueberries", 1));
            iconSignPiece.AddRequirement(new RequirementConfig("Guck", 1));

            iconSignPiece.Icon = SpriteLoader.LoadBuildPieceIcon();

            Translations.AddToLocalizationManager();

            var customPiece = new CustomPiece(BuildPieceName, "sign", iconSignPiece);
            PieceManager.Instance.AddPiece(customPiece);
            customPiece.PiecePrefab.gameObject.AddComponent<IconSign>();

            Logger.LogInfo("icon sign created");
        }
    }
}