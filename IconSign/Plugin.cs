using BepInEx;
using IconSign.Data;
using IconSign.Sign;
using Jotunn;
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


        private void Awake()
        {
            RecentIcons.ConfigEntry = Config.Bind("config", "recent_icons", "", "your recently used icons");

            PrefabManager.OnVanillaPrefabsAvailable += IconSignFactory.CreateIconSign;
        }
    }
}